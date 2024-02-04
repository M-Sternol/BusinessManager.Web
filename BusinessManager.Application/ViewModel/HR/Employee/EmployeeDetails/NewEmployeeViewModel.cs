using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.Education;
using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using BusinessManager.Domain.Models.HR.Employee.Contact;
using BusinessManager.Domain.Models.HR.Employee.Education;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Models.HR.Employee.Work.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails
{
    public class NewEmployeeViewModel : IMapFrom<EmployeeModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PESEL { get; set; }
        public List<EmployeeAddressViewModel> Addresses { get; set; } = new List<EmployeeAddressViewModel>();
        public List<EmployeeContactViewModel> Contacts { get; set; } = new List<EmployeeContactViewModel>();
        public List<EmployeeEducationViewModel> Educations { get; set; } = new List<EmployeeEducationViewModel>();
        public List<EmploymentContractViewModel> Employments { get; set; } = new List<EmploymentContractViewModel>();
        public List<EmergencyContactViewModel> EmergencyContacts { get; set; } = new List<EmergencyContactViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeViewModel, EmployeeModel>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(scr => scr.Contacts))
                .ForMember(dest => dest.Educations, opt => opt.MapFrom(scr => scr.Educations))
                .ForMember(dest => dest.EmploymentContracts, opt => opt.MapFrom(scr => scr.Employments))
                .ForMember(dest => dest.EmergencyContacts, opt => opt.MapFrom(scr => scr.EmergencyContacts))
                .ReverseMap();
        }
    }
}
