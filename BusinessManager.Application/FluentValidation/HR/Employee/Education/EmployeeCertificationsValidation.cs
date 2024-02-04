using BusinessManager.Application.ViewModel.HR.Employee.Education;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Education
{
    public class EmployeeCertificationsValidation : AbstractValidator<EmployeeCertificationViewModel>
    {
        public EmployeeCertificationsValidation()
        {
            RuleFor(certification => certification.CertificationName)
                .NotEmpty().WithMessage("Certification name is required.")
                .MaximumLength(50).WithMessage("Certification name cannot exceed 50 characters.");
        }
    }
}
