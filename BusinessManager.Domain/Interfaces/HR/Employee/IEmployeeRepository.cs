using BusinessManager.Domain.Models.HR.Employee.Contact;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Interfaces.HR.Employee
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployee(EmployeeModel employee);
        EmployeeModel EmployeeExists(string email, string phoneNumber, string pesel);
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        IQueryable<EmployeeModel> GetAllEmployees();
        void UpdateEmployee(EmployeeModel employee);
        Task DeleteEmployeeAsync(EmployeeModel employee);
    }
}
