using AutoMapper;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.Interfaces.Settings;
using BusinessManager.Application.ViewModel.ErrorViewModel;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;

namespace BusinessManager.Application.Services.HR.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IGenerateStrongPassword _strongPassword;
        private readonly IRoleInitializer _roleInitializer;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IEmailService emailService,
            IGenerateStrongPassword strongPassword,
            IRoleInitializer roleInitializer,
            RoleManager<IdentityRole> roleManager)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _strongPassword = strongPassword;
            _roleInitializer = roleInitializer;
            _roleManager = roleManager;
        }

        public async Task<int> AddEmployeeAsync(NewEmployeeViewModel newEmployeeVm)
        {
            if (string.IsNullOrWhiteSpace(newEmployeeVm?.Contacts.FirstOrDefault()?.Email))
            {
                throw new InvalidOperationException("Email nie może być pusty.");
            }

            var email = newEmployeeVm.Contacts.FirstOrDefault()?.Email;

            await CheckIfUserExistsAsync(email);

            // Generate a strong password
            var password = _strongPassword.GeneratePassword();

            // Create a new identity user
            var newUser = new IdentityUser { UserName = email, Email = email };

            // Attempt to create the user asynchronously
            var createUserResult = await CreateUserAsync(newUser, password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join("; ", createUserResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Nie udało się utworzyć użytkownika: {errors}");
            }

            // Attempt to add role "Employee" to the user
            var addRoleResult = await _userManager.AddToRoleAsync(newUser, "Employee");
            if (!addRoleResult.Succeeded)
            {
                var errors = string.Join("; ", addRoleResult.Errors.Select(e => e.Description));
                // Consider how to handle this situation - maybe delete the user or retry the role assignment
            }

            var newEmployee = _mapper.Map<EmployeeModel>(newEmployeeVm);
            newEmployee.Position = newEmployeeVm.Employments.FirstOrDefault()?.Position;

            // Add the new employee to the repository
            var id = await _employeeRepository.AddEmployee(newEmployee);

            // Attempt to send login information via email
            try
            {
                await _emailService.SendLoginEmailAsync(email, newEmployee.FirstName, newEmployee.LastName, password);
            }
            catch (Exception ex)
            {
                // Decide how to handle this - maybe log the error and continue, or indicate the issue to the user/caller
                throw new InvalidOperationException("Błąd podczas wysyłania informacji o logowaniu przez e-mail.", ex);
            }

            return id;
        }


        public UniquenessCheckResult IsInformationUnique(string email, string phoneNumber, string firstName, string lastName, DateTime birthDate, string pesel)
        {
            var existingEmployee = _employeeRepository.EmployeeExists(email, phoneNumber, pesel);
            if (existingEmployee != null)
            {
                if (existingEmployee.Contacts != null && existingEmployee.Contacts.Any(c => c.Email == email))
                {
                    return new UniquenessCheckResult { IsUnique = false, ConflictField = "Email" };
                }
                if (existingEmployee.Contacts != null && existingEmployee.Contacts.Any(c => c.PhoneNumber == phoneNumber))
                {
                    return new UniquenessCheckResult { IsUnique = false, ConflictField = "PhoneNumber" };
                }
                if (existingEmployee.PESEL == pesel)
                {
                    return new UniquenessCheckResult { IsUnique = false, ConflictField = "PESEL" };
                }
            }

            return new UniquenessCheckResult { IsUnique = true };
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                {
                    return false;
                }

                await _employeeRepository.DeleteEmployeeAsync(employee);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                return employee != null ? _mapper.Map<EmployeeViewModel>(employee) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Błąd podczas pobierania danych pracownika o ID: {employeeId}", ex);
            }
        }

        public async Task<IPagedList<EmployeeForListViewModel>> GetEmployeesListAsync(int pageNumber, int pageSize, string searchString, string sortOrder)
        {
            try
            {
                var query = _employeeRepository.GetAllEmployees();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var searchStringLower = searchString.ToLower();
                    query = query.Where(e => (e.FirstName + " " + e.LastName).ToLower().Contains(searchStringLower));
                }

                sortOrder = string.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;

                query = sortOrder switch
                {
                    "name_desc" => query.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName),
                    "position_desc" => query.OrderByDescending(e => e.Position),
                    "position" => query.OrderBy(e => e.Position),
                    "name" => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName),
                    _ => query.OrderBy(e => e.LastName)
                };

                var totalEmployees = await query.CountAsync();
                var employees = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                var employeeViewModels = employees.Select(e => new EmployeeForListViewModel
                {
                    Id = e.Id,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Position = e.Position
                }).ToList();

                return new StaticPagedList<EmployeeForListViewModel>(employeeViewModels, pageNumber, pageSize, totalEmployees);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public async Task<NewEmployeeViewModel> GetEmployeeForEditAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                var employeeViewModel = _mapper.Map<NewEmployeeViewModel>(employee);
                return employeeViewModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public void UpdateEmployee(NewEmployeeViewModel model)
        {
            try
            {
                var employee = _mapper.Map<EmployeeModel>(model);
                _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        private async Task CheckIfUserExistsAsync(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Użytkownik o adresie e-mail {email} już istnieje.");
            }
        }

        private async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}