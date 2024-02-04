using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Contact
{
    public class AddressValidation : AbstractValidator<EmployeeAddressViewModel>
    {
        public AddressValidation()
        {
            RuleFor(address => address.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(address => address.Region)
                .MaximumLength(50).WithMessage("Region cannot exceed 50 characters.");

            RuleFor(address => address.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .Matches("^\\d{2}-\\d{3}$").WithMessage("Invalid postal code format.");

            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street cannot exceed 100 characters.");

            RuleFor(address => address.BuildingNumber)
                .NotEmpty().WithMessage("Building number is required.")
                .MaximumLength(20).WithMessage("Building number cannot exceed 20 characters.");

            RuleFor(address => address.FlatNumber)
                .MaximumLength(20).WithMessage("Flat number cannot exceed 20 characters.");
        }
    }
}
