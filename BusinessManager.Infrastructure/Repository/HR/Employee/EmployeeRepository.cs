using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using BusinessManager.Infrastructure.DbContext.HR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessManager.Infrastructure.Repository.HR.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddEmployee(EmployeeModel employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public EmployeeModel EmployeeExists(string email, string phoneNumber, string pesel)
        {
            if (_context.Employees == null) return null;

            var employeeWithEmail = _context.Employees.FirstOrDefault(e => e.Contacts != null && e.Contacts.Any(c => c.Email == email));
            if (employeeWithEmail != null) return employeeWithEmail;

            var employeeWithPhoneNumber = _context.Employees.FirstOrDefault(e => e.Contacts != null && e.Contacts.Any(c => c.PhoneNumber == phoneNumber));
            if (employeeWithPhoneNumber != null) return employeeWithPhoneNumber;

            var employeeWithPesel = _context.Employees.FirstOrDefault(e => e.PESEL == pesel);
            return employeeWithPesel;
        }


        public async Task DeleteEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await _context.Employees
                .Include(e => e.Addresses)
                .Include(e => e.Contacts)
                .Include(e => e.Educations)
                .Include(e => e.EmploymentContracts)
                .Include(e => e.EmergencyContacts)
                .Include(e => e.LeaveRequests)
                .Include(e => e.MedicalLeaves)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public IQueryable<EmployeeModel> GetAllEmployees()
        {
            try
            {
                return _context.Employees;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                _context.Attach(employee);
                _context.Entry(employee).State = EntityState.Modified;
                _context.Entry(employee).Property(e => e.Position).IsModified = false;

                // Aktualizacja danych kontaktowych i adresowych
                foreach (var contact in employee.Contacts)
                {
                    _context.Entry(contact).State = contact.Id == 0 ? EntityState.Added : EntityState.Modified;
                }
                foreach (var address in employee.Addresses)
                {
                    _context.Entry(address).State = address.Id == 0 ? EntityState.Added : EntityState.Modified;
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}