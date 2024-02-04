using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using System.Collections.Generic;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave
{
    public class MedicalLeaveListViewModel
    { 
        public IEnumerable<MedicalLeaveViewModel> MedicalLeaves { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalEmployeeCount { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
    }
}
