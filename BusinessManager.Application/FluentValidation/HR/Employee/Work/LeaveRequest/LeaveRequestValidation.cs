using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Work.LeaveRequest
{
    public class LeaveRequestValidation : AbstractValidator<LeaveRequestViewModel>
    {
        public LeaveRequestValidation()
        {
            RuleFor(request => request.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .Must(BeAValidDateTime).WithMessage("Invalid start date.");

            RuleFor(request => request.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .Must(BeAValidDateTime).WithMessage("Invalid end date")
                .GreaterThanOrEqualTo(request => request.StartDate).WithMessage("End date must be greater than or equal to start date.");

            RuleFor(request => request.Type)
                .IsInEnum().WithMessage("Invalid leave type.")
                .NotNull().WithMessage("Leave type is required.");

            RuleFor(request => request.Status)
                .IsInEnum().WithMessage("Invalid request status.")
                .NotNull().WithMessage("Request status is required.");

        }

        private bool BeAValidDateTime(DateTime dateTime)
        {
            return dateTime < DateTime.Now;
        }
    }
}
