using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.Education;
using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using PagedList;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BusinessManager.Web.Controllers.HR.Employee
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger,RoleManager<IdentityRole> roleManager)
        {
            _employeeService = employeeService;
            _logger = logger;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                ViewBag.PositionSortParm = sortOrder == "position_desc" ? "position" : "position_desc";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var pageSize = 10;
                var pageNumber = page ?? 1;
                var pagedEmployees = await _employeeService.GetEmployeesListAsync(pageNumber, pageSize, searchString, sortOrder);

                return View(pagedEmployees);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            try
            {
                var model = new NewEmployeeViewModel();
                for (int i = 0; i < 1; i++)
                {
                    var educationViewModel = new EmployeeEducationViewModel();
                    educationViewModel.Certifications.Add(new EmployeeCertificationViewModel());
                    educationViewModel.Languages.Add(new EmployeeLanguageViewModel());
                    educationViewModel.Skills.Add(new EmployeeSkillViewModel());

                    model.Addresses.Add(new EmployeeAddressViewModel());
                    model.Contacts.Add(new EmployeeContactViewModel());
                    model.Educations.Add(educationViewModel);
                    model.Employments.Add(new EmploymentContractViewModel());
                    model.EmergencyContacts.Add(new EmergencyContactViewModel());
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(NewEmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if Employments is null or empty
                    if (model.Employments == null || !model.Employments.Any())
                    {
                        throw new InvalidOperationException("Lista Employments jest pusta.");
                    }

                    // Use null-conditional operators to avoid null reference exceptions
                    var email = model.Contacts?[0]?.Email;
                    var phoneNumber = model.Contacts?[0]?.PhoneNumber;
                    var uniquenessCheckResult = _employeeService.IsInformationUnique(email, phoneNumber, model.FirstName, model.LastName, model.BirthDate, model.PESEL);

                    if (!uniquenessCheckResult.IsUnique)
                    {
                        string errorMessage = $"Pracownik o tym samym {uniquenessCheckResult.ConflictField} już istnieje.";
                        ModelState.AddModelError(uniquenessCheckResult.ConflictField, errorMessage);
                    }
                    else
                    {
                        await _employeeService.AddEmployeeAsync(model);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas tworzenia pracownika");
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await _employeeService.DeleteEmployeeAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                int employeeId = id;
                ViewData["EmployeeId"] = employeeId;
                return View(employee);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeForEditAsync(id);
                if(employee == null)
                {
                    return NotFound();
                }
                if (employee.Addresses.Count == 0)
                {
                    employee.Addresses.Add(new EmployeeAddressViewModel());
                }
                if (employee.Contacts.Count == 0)
                {
                    employee.Contacts.Add(new EmployeeContactViewModel());
                }
                if (employee.EmergencyContacts.Count == 0)
                {
                    employee.EmergencyContacts.Add(new EmergencyContactViewModel());
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> EditEmployee(NewEmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.UpdateEmployee(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        public IActionResult Error()
        {
            return View();
        }

    }
}
