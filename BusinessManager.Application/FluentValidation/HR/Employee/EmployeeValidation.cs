using BusinessManager.Application.FluentValidation.HR.Employee.Contact;
using BusinessManager.Application.FluentValidation.HR.Employee.Education;
using BusinessManager.Application.FluentValidation.HR.Employee.Work.Contract;
using BusinessManager.Application.FluentValidation.HR.Employee.Work.LeaveRequest;
using BusinessManager.Application.FluentValidation.HR.Employee.Work.MedicalLeave;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee
{
    public class EmployeeValidation : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeValidation()
        {
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(employee => employee.PESEL)
                .NotEmpty().WithMessage("PESEL is required.")
                .Length(11).WithMessage("PESEL must be exactly 11 digits.")
                .Matches("^[0-9]{11}$").WithMessage("PESEL must contain only digits.");

            RuleFor(employee => employee.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage("Employee must be at least 18 years old.")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Employee cannot be more than 100 years old.");

            RuleFor(employee => employee.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position cannot exceed 100 characters.");

            RuleForEach(employee => employee.EmergencyContacts)
                .SetValidator(new EmergencyContactValidation()); // Assuming a validator for EmergencyContactViewModel exists

            RuleForEach(employee => employee.Addresses)
                .SetValidator(new AddressValidation()); // Assuming a validator for EmployeeAddressViewModel exists

            RuleForEach(employee => employee.Contacts)
                .SetValidator(new ContactValidation()); // Assuming a validator for EmployeeContactViewModel exists

            RuleForEach(employee => employee.Educations)
                .SetValidator(new EmployeeEducationValidation()); // Assuming a validator for EmployeeEducationViewModel exists

            RuleForEach(employee => employee.EmploymentContracts)
                .SetValidator(new EmploymentContractValidation()); // Assuming a validator for EmploymentContractViewModel exists

            RuleForEach(employee => employee.LeaveRequests)
                .SetValidator(new LeaveRequestValidation()); // Assuming a validator for LeaveRequestViewModel exists

            RuleForEach(employee => employee.MedicalLeaves)
                .SetValidator(new MedicalLeavesValidation()); // Assuming a validator for MedicalLeaveViewModel exists
        }

    }
}
