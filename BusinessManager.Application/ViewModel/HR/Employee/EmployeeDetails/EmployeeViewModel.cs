using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.Education;
using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;


namespace BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails
{
    public class EmployeeViewModel : IMapFrom<EmployeeModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PESEL { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }

        public ICollection<EmergencyContactViewModel> EmergencyContacts { get; set; }
        public ICollection<EmployeeAddressViewModel> Addresses { get; set; }
        public ICollection<EmployeeContactViewModel> Contacts { get; set; }
        public ICollection<EmployeeEducationViewModel> Educations { get; set; }
        public ICollection<EmploymentContractViewModel> EmploymentContracts { get; set; }
        public ICollection<LeaveRequestViewModel> LeaveRequests { get; set; }
        public ICollection<MedicalLeaveViewModel> MedicalLeaves { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeModel, EmployeeViewModel>()
                .ForMember(dest => dest.EmergencyContacts, opt => opt.MapFrom(src => src.EmergencyContacts))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
                .ForMember(dest => dest.EmploymentContracts, opt => opt.MapFrom(src => src.EmploymentContracts))
                .ForMember(dest => dest.LeaveRequests, opt => opt.MapFrom(src => src.LeaveRequests))
                .ForMember(dest => dest.MedicalLeaves, opt => opt.MapFrom(src => src.MedicalLeaves));
        }
    }
}
