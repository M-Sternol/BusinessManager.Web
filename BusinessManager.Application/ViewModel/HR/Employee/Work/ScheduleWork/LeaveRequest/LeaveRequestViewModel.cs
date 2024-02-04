using AutoMapper;
using BusinessManager.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest
{
    public class LeaveRequestViewModel : IMapFrom<Domain.Models.HR.Employee.Work.ScheduleWork.LeaveRequest>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus Status { get; set; }
        public int EmployeeId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.HR.Employee.Work.ScheduleWork.LeaveRequest, LeaveRequestViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (LeaveRequestStatus)src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (LeaveType)src.Type))
                .ReverseMap();
        }
    }

    public enum LeaveRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum LeaveType
    {
        Vacation,
        UnpaidLeave,
        OnDemand
    }
}
