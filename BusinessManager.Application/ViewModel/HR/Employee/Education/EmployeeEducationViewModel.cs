using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Education;

namespace BusinessManager.Application.ViewModel.HR.Employee.Education
{
    public class EmployeeEducationViewModel : IMapFrom<EmployeeEducation>
    {
        public int Id { get; set; }
        public EducationLevelVm EducationLevel { get; set; }
        public string SchoolName { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool IsCurrentlyEnrolled { get; set; }
        public List<EmployeeLanguageViewModel> Languages { get; set; } = new List<EmployeeLanguageViewModel>();
        public List<EmployeeSkillViewModel> Skills { get; set; } = new List<EmployeeSkillViewModel>();
        public List<EmployeeCertificationViewModel> Certifications { get; set; } = new List<EmployeeCertificationViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeEducation, EmployeeEducationViewModel>()
                .ForMember(dest => dest.EducationLevel, opt => opt.MapFrom(src => (EducationLevelVm)src.EducationLevel))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
                .ReverseMap();
        }
    }

    public enum EducationLevelVm
    {
        Primary,
        Secondary,
        Higher
    }
}