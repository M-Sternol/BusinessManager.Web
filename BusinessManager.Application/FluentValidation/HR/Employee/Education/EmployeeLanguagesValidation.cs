using BusinessManager.Application.ViewModel.HR.Employee.Education;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Education
{
    public class EmployeeLanguagesValidation : AbstractValidator<EmployeeLanguageViewModel>
    {
        public EmployeeLanguagesValidation()
        {
            RuleFor(language => language.LanguageName)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(50).WithMessage("Language name cannot exceed 50 characters.");

            RuleFor(language => language.ProficiencyLevel)
                .IsInEnum().WithMessage("Proficiency level is invalid.")
                .NotNull().WithMessage("Language level is required.");
        }
    }
}
