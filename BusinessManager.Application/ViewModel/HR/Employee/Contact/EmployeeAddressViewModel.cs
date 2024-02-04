using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Contact;

namespace BusinessManager.Application.ViewModel.HR.Employee.Contact
{
    public class EmployeeAddressViewModel : IMapFrom<EmployeeAddress>
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeAddressViewModel, EmployeeAddress>().ReverseMap();

        }


    }
}