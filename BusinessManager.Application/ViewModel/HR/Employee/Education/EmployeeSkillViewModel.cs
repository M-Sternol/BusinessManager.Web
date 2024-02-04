using AutoMapper;
using BusinessManager.Application.Mapper;
using BusinessManager.Domain.Models.HR.Employee.Education;

namespace BusinessManager.Application.ViewModel.HR.Employee.Education
{
    public class EmployeeSkillViewModel : IMapFrom<EmployeeSkill>
    {
        public int Id { get; set; }
        public string SkillName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeSkill, EmployeeSkillViewModel>().ReverseMap(); ;
                
        }
    }
}