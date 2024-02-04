using Xunit;
using FluentAssertions;
using BusinessManager.Application.FluentValidation.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;

namespace BusinessManager.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            // Arrange
            var validator = new EmployeeValidation();
            var employee = new EmployeeViewModel { FirstName = string.Empty };

            // Act
            var result = validator.Validate(employee);

            // Assert
            result.Errors.Should().Contain(error => error.PropertyName == nameof(EmployeeViewModel.FirstName));
        }
        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            // Arrange
            var validator = new EmployeeValidation();
            var employee = new EmployeeViewModel { LastName = string.Empty };

            // Act
            var result = validator.Validate(employee);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error =>
                error.PropertyName == nameof(EmployeeViewModel.LastName) &&
                error.ErrorMessage == "Last name is required."
            );
        }
        [Fact]
        public void Should_Have_Error_When_PESEL_Is_Invalid()
        {
            // Arrange
            var validator = new EmployeeValidation();
            var employee = new EmployeeViewModel { PESEL = "123" }; // Niepoprawny PESEL

            // Act
            var result = validator.Validate(employee);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error =>
                error.PropertyName == nameof(EmployeeViewModel.PESEL) &&
                error.ErrorMessage == "PESEL must be exactly 11 digits."
            );
        }
        [Fact]
        public void Should_Have_Error_When_BirthDate_Indicates_Too_Young()
        {
            // Arrange
            var validator = new EmployeeValidation();
            var employee = new EmployeeViewModel { BirthDate = DateTime.Now.AddYears(-10) }; // Zbyt młoda osoba

            // Act
            var result = validator.Validate(employee);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error =>
                error.PropertyName == nameof(EmployeeViewModel.BirthDate) &&
                error.ErrorMessage == "Employee must be at least 18 years old."
            );
        }


    }
}
