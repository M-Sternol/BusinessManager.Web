using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Work.Contract
{
    internal class EmploymentContractValidation : AbstractValidator<EmploymentContractViewModel>
    {
        public EmploymentContractValidation() 
        {
            RuleFor(contract => contract.DurationMonths)
                .GreaterThan(0).WithMessage("Duration in months must be greater than 0.");

            RuleFor(contract => contract.Position)
                .NotEmpty().WithMessage("Position is required.")
                .Length(2, 100).WithMessage("Position must be between 2 and 100 characters.");

            RuleFor(contract => contract.HourlyRate)
                .GreaterThan(0).WithMessage("Hourly rate must be greater than 0.");

            RuleFor(contract => contract.MonthlyRate)
                .GreaterThan(0).WithMessage("Monthly rate must be greater than 0.");

        }
    }
}
