using BusinessManager.Application.ViewModel.ErrorViewModel;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using Microsoft.AspNetCore.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Interfaces.HR.Employee
{
    public interface IEmployeeService
    {
        Task<IPagedList<EmployeeForListViewModel>> GetEmployeesListAsync(int pageNumber, int pageSize, string searchString, string sortOrder);
        UniquenessCheckResult IsInformationUnique(string email, string phoneNumber, string firstName, string lastName, DateTime birthDate, string pesel);
        Task<EmployeeViewModel> GetEmployeeByIdAsync(int employeeId);
        Task<int> AddEmployeeAsync(NewEmployeeViewModel newEmployeeVm);
        Task<NewEmployeeViewModel> GetEmployeeForEditAsync(int id);
        void UpdateEmployee(NewEmployeeViewModel employeeViewModel);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
