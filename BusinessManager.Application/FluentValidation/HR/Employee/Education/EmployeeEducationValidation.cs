using BusinessManager.Application.ViewModel.HR.Employee.Education;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Education
{
    public class EmployeeEducationValidation : AbstractValidator<EmployeeEducationViewModel>
    {
        public EmployeeEducationValidation()
        {
            RuleFor(education => education.EducationLevel)
                .IsInEnum().WithMessage("Invalid education level.");

            RuleFor(education => education.SchoolName)
                .NotEmpty().WithMessage("School name is required.")
                .MaximumLength(100).WithMessage("School name cannot exceed 100 characters.");

            RuleFor(education => education.GraduationDate)
                .NotEmpty().WithMessage("Graduation date is required.")
                .Must(BeAValidDate).WithMessage("Invalid graduation date.");

            RuleForEach(education => education.Languages)
                .SetValidator(new EmployeeLanguagesValidation());

            RuleForEach(education => education.Skills)
                .SetValidator(new EmployeeSkillsValidation());

            RuleForEach(education => education.Certifications)
                .SetValidator(new EmployeeCertificationsValidation());
        }

        private bool BeAValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
