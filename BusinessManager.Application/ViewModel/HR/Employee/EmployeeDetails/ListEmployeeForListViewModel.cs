using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails
{
    public class ListEmployeeForListViewModel
    {
        public IEnumerable<EmployeeForListViewModel> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalEmployeeCount { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
    }

}
