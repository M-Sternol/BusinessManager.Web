using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using FluentValidation;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Contact
{
    public class EmergencyContactValidation : AbstractValidator<EmergencyContactViewModel>
    {
        public EmergencyContactValidation()
        {
            RuleFor(contact => contact.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(contact => contact.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(contact => contact.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required.")
               .Must(BeAValidPhoneNumber).WithMessage("Invalid phone number format.");

            RuleFor(contact => contact.Relationship)
                .NotEmpty().WithMessage("Relationship is required.")
                .MaximumLength(50).WithMessage("Relationship cannot exceed 50 characters.");
        }
        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, null);
                return phoneNumberUtil.IsValidNumber(parsedPhoneNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
