using AutoMapper;
using BusinessManager.Application.Interfaces.Settings;
using BusinessManager.Application.Services.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using Microsoft.AspNetCore.Http;

namespace BusinessManager.Tests.HR.Employee
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IGenerateStrongPassword> _mockStrongPassword;
        private readonly Mock<IRoleInitializer> _mockRoleInitializer;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;

        public EmployeeServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUserManager = MockUserManager();
            _mockEmailService = new Mock<IEmailService>();
            _mockStrongPassword = new Mock<IGenerateStrongPassword>();
            _mockRoleInitializer = new Mock<IRoleInitializer>();
            _mockRoleManager = MockRoleManager();
        }

        private Mock<UserManager<IdentityUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<RoleManager<IdentityRole>> MockRoleManager()
        {
            var store = new Mock<IRoleStore<IdentityRole>>();
            return new Mock<RoleManager<IdentityRole>>(store.Object, null, null, null, null);
        }
        [Fact]
        public async Task AddEmployeeAsync_Success()
        {
            // Arrange
            var newEmployeeVm = new NewEmployeeViewModel
            {
                Contacts = new List<EmployeeContactViewModel> { new EmployeeContactViewModel { Email = "test@example.com" } }
            };
            var employeeModel = new EmployeeModel();
            _mockMapper.Setup(m => m.Map<EmployeeModel>(It.IsAny<NewEmployeeViewModel>())).Returns(employeeModel);

            _mockStrongPassword.Setup(s => s.GeneratePassword()).Returns("StrongPassword123!");
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);
            _mockEmployeeRepository.Setup(er => er.AddEmployee(It.IsAny<EmployeeModel>()))
                                   .ReturnsAsync(1); // assuming 1 is the new employee ID
            _mockEmailService.Setup(es => es.SendLoginEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(Task.CompletedTask);

            var service = new EmployeeService(_mockEmployeeRepository.Object,
                                              _mockMapper.Object,
                                              _mockUserManager.Object,
                                              _mockEmailService.Object,
                                              _mockStrongPassword.Object,
                                              _mockRoleInitializer.Object,
                                              _mockRoleManager.Object);

            // Act
            var result = await service.AddEmployeeAsync(newEmployeeVm);

            // Assert
            Assert.Equal(1, result); // Asserting that the returned ID is as expected
        }


        [Fact]
        public async Task DeleteEmployeeAsync_ValidEmployeeId_ShouldReturnTrue()
        {
            // Arrange
            var employeeId = 1;

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new EmployeeModel()); // Return a valid employee for the given id
            employeeRepositoryMock.Setup(repo => repo.DeleteEmployeeAsync(It.IsAny<EmployeeModel>()))
                .Returns(Task.CompletedTask);

            var employeeService = new EmployeeService(
                employeeRepositoryMock.Object,
                It.IsAny<IMapper>(),
                It.IsAny<UserManager<IdentityUser>>(),
                It.IsAny<IEmailService>(),
                It.IsAny<IGenerateStrongPassword>(),
                It.IsAny<IRoleInitializer>(),
                It.IsAny<RoleManager<IdentityRole>>());

            // Act
            var result = await employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ValidEmployeeId_ShouldReturnEmployeeViewModel()
        {
            // Arrange
            var employeeId = 1;
            var employeeModel = new EmployeeModel { /* Initialize with valid data */ };

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(employeeModel);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<EmployeeViewModel>(employeeModel))
                .Returns(new EmployeeViewModel { /* Initialize with valid data */ });

            var employeeService = new EmployeeService(
                employeeRepositoryMock.Object,
                mapperMock.Object,
                It.IsAny<UserManager<IdentityUser>>(),
                It.IsAny<IEmailService>(),
                It.IsAny<IGenerateStrongPassword>(),
                It.IsAny<IRoleInitializer>(),
                It.IsAny<RoleManager<IdentityRole>>());

            // Act
            var result = await employeeService.GetEmployeeByIdAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EmployeeViewModel>(result);
        }

        [Fact]
        public async Task GetEmployeeForEditAsync_ValidEmployeeId_ShouldReturnNewEmployeeViewModel()
        {
            // Arrange
            var employeeId = 1;
            var employeeModel = new EmployeeModel { /* Initialize with valid data */ };

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(employeeModel);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<NewEmployeeViewModel>(employeeModel))
                .Returns(new NewEmployeeViewModel { /* Initialize with valid data */ });

            var employeeService = new EmployeeService(
                employeeRepositoryMock.Object,
                mapperMock.Object,
                It.IsAny<UserManager<IdentityUser>>(),
                It.IsAny<IEmailService>(),
                It.IsAny<IGenerateStrongPassword>(),
                It.IsAny<IRoleInitializer>(),
                It.IsAny<RoleManager<IdentityRole>>());

            // Act
            var result = await employeeService.GetEmployeeForEditAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NewEmployeeViewModel>(result);
        }

        [Fact]
        public void UpdateEmployee_ValidModel_ShouldNotThrowException()
        {
            // Arrange
            var model = new NewEmployeeViewModel { /* Initialize with valid data */ };

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(repo => repo.UpdateEmployee(It.IsAny<EmployeeModel>()))
                .Verifiable();

            var employeeService = new EmployeeService(
                employeeRepositoryMock.Object,
                It.IsAny<IMapper>(),
                It.IsAny<UserManager<IdentityUser>>(),
                It.IsAny<IEmailService>(),
                It.IsAny<IGenerateStrongPassword>(),
                It.IsAny<IRoleInitializer>(),
                It.IsAny<RoleManager<IdentityRole>>());
        }
    }
}
