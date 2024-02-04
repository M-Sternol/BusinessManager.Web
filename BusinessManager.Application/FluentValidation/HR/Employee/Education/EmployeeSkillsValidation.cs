using BusinessManager.Application.ViewModel.HR.Employee.Education;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Education
{
    public class EmployeeSkillsValidation : AbstractValidator<EmployeeSkillViewModel>
    {
        public EmployeeSkillsValidation()
        {
            RuleFor(skill => skill.SkillName)
                .NotEmpty().WithMessage("Skill name is required.")
                .MaximumLength(50).WithMessage("Skill name cannot exceed 50 characters.");
        }
    }
}
