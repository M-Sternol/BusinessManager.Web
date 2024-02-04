using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Contact;

namespace BusinessManager.Application.ViewModel.HR.Employee.Contact
{
    public class EmployeeContactViewModel : IMapFrom<EmployeeContact>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeContact, EmployeeContactViewModel>().ReverseMap();
        }
    }
}