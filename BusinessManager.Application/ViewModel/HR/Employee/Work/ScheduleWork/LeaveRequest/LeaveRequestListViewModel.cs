using AutoMapper;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using System.Collections.Generic;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest
{
    public class LeaveRequestListViewModel
    {
        public IEnumerable<LeaveRequestForListViewModel> LeaveRequestForLists { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalEmployeeCount { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }

    }
}
