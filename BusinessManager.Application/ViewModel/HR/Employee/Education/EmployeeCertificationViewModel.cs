using AutoMapper;
using BusinessManager.Application.Mapper;

namespace BusinessManager.Application.ViewModel.HR.Employee.Education
{
    public class EmployeeCertificationViewModel : IMapFrom<Domain.Models.HR.Employee.Education.EmployeeCertification>
    {
        public int Id { get; set; }
        public string CertificationName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.HR.Employee.Education.EmployeeCertification, EmployeeCertificationViewModel>().ReverseMap();
        }
    }
}