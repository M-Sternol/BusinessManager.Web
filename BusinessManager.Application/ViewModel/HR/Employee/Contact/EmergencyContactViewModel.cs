using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Contact;

namespace BusinessManager.Application.ViewModel.HR.Employee.Contact
{
    public class EmergencyContactViewModel : IMapFrom<EmergencyContactViewModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmergencyContact, EmergencyContactViewModel>().ReverseMap();
        }
    }
}