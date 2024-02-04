using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Work.MedicalLeave
{
    internal class MedicalLeavesValidation : AbstractValidator<MedicalLeaveViewModel>
    {
        public MedicalLeavesValidation() 
        {
            RuleFor(medicalLeave => medicalLeave.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(medicalLeave => medicalLeave.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(medicalLeave => medicalLeave.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(medicalLeave => medicalLeave.StartDate)
                .LessThan(medicalLeave => medicalLeave.EndDate).WithMessage("Start date must be before the end date.")
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future.");

            RuleFor(medicalLeave => medicalLeave.EndDate)
                .GreaterThan(medicalLeave => medicalLeave.StartDate).WithMessage("End date must be after the start date.");

        }
    }
}
