using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Domain.Models.HR.Employee.Contact;
using FluentValidation;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.FluentValidation.HR.Employee.Contact
{
    public class ContactValidation : AbstractValidator<EmployeeContactViewModel>
    {
        public ContactValidation()
        {
            RuleFor(contact => contact.Email)
                 .NotEmpty().WithMessage("Email is required.")
                 .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                 .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").WithMessage("Invalid email format.");

            RuleFor(contact => contact.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(BeAValidPhoneNumber).WithMessage("Invalid phone number format.");
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
