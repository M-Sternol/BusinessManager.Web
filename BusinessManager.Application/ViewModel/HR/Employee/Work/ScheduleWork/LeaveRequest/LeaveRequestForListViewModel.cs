using AutoMapper;
using BusinessManager.Application.Mapper;

namespace BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest
{
    public class LeaveRequestForListViewModel : IMapFrom<Domain.Models.HR.Employee.Work.ScheduleWork.LeaveRequest>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.HR.Employee.Work.ScheduleWork.LeaveRequest, LeaveRequestForListViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }

    }
}