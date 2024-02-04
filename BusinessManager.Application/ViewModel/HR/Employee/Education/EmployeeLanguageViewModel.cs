using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Education;

namespace BusinessManager.Application.ViewModel.HR.Employee.Education
{
    public class EmployeeLanguageViewModel : IMapFrom<EmployeeLanguage>
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public LanguageProficiency ProficiencyLevel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeLanguage, EmployeeLanguageViewModel>().ReverseMap(); ;
        }
    }
    public enum LanguageProficiency
    {
        A1,
        A2,
        B1,
        B2,
        C1,
        C2
    }
}