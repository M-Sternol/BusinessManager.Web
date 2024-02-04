using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.Services.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Moq;
using PagedList;
using Xunit;

namespace BusinessManager.Tests
{
    public class LeaveRequestServiceTests
    {
        [Fact]
        public async Task CreateLeaveRequestAsync_ShouldCreateLeaveRequest()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var leaveRequestRepository = new Mock<ILeaveRequestRepository>();
            var employeeRepository = new Mock<IEmployeeRepository>();
            var leaveRequestService = new LeaveRequestService(mapper.Object, leaveRequestRepository.Object, employeeRepository.Object);
            var leaveRequestViewModel = new LeaveRequestViewModel(); // Replace with a valid LeaveRequestViewModel.

            var newLeaveRequest = new LeaveRequest(); // Replace with a valid LeaveRequest object.
            mapper.Setup(m => m.Map<LeaveRequest>(leaveRequestViewModel)).Returns(newLeaveRequest);
            leaveRequestRepository.Setup(r => r.AddLeaveRequestAsync(newLeaveRequest)).ReturnsAsync(1); // Replace with a valid ID.

            // Act
            var result = await leaveRequestService.CreateLeaveRequestAsync(leaveRequestViewModel);

            // Assert
            Assert.NotEqual(0, result);
            leaveRequestRepository.Verify(r => r.AddLeaveRequestAsync(newLeaveRequest), Times.Once); // Verify that the repository method was called once.
        }

        [Fact]
        public async Task GetLeaveRequestByIdAsync_ShouldReturnLeaveRequest()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var leaveRequestRepository = new Mock<ILeaveRequestRepository>();
            var employeeRepository = new Mock<IEmployeeRepository>();
            var leaveRequestService = new LeaveRequestService(mapper.Object, leaveRequestRepository.Object, employeeRepository.Object);
            var leaveRequestId = 1; // Replace with a valid leave request ID.
            var leaveRequest = new LeaveRequest(); // Replace with a valid LeaveRequest object.

            leaveRequestRepository.Setup(r => r.GetLeaveRequestByIdAsync(leaveRequestId)).ReturnsAsync(leaveRequest);
            mapper.Setup(m => m.Map<LeaveRequestViewModel>(leaveRequest)).Returns(new LeaveRequestViewModel());

            // Act
            var result = await leaveRequestService.GetLeaveRequestByIdAsync(leaveRequestId);

            // Assert
            Assert.NotNull(result);
            leaveRequestRepository.Verify(r => r.GetLeaveRequestByIdAsync(leaveRequestId), Times.Once); // Verify that the repository method was called once.
        }

        [Fact]
        public async Task DeleteLeaveRequestAsync_ShouldDeleteLeaveRequest()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var leaveRequestRepository = new Mock<ILeaveRequestRepository>();
            var employeeRepository = new Mock<IEmployeeRepository>();
            var leaveRequestService = new LeaveRequestService(mapper.Object, leaveRequestRepository.Object, employeeRepository.Object);

            var leaveRequestId = 1; // Replace with a valid leave request ID.
            var leaveRequest = new LeaveRequest(); // Replace with a valid LeaveRequest object.

            leaveRequestRepository.Setup(r => r.GetLeaveRequestByIdAsync(leaveRequestId)).ReturnsAsync(leaveRequest);
            leaveRequestRepository.Setup(r => r.DeleteLeaveRequestAsync(leaveRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await leaveRequestService.DeleteLeaveRequestAsync(leaveRequestId);

            // Assert
            Assert.True(result);
            leaveRequestRepository.Verify(r => r.GetLeaveRequestByIdAsync(leaveRequestId), Times.Once); // Verify that the repository method was called once.
            leaveRequestRepository.Verify(r => r.DeleteLeaveRequestAsync(leaveRequest), Times.Once); // Verify that the repository method was called once.
        }

        [Fact]
        public async Task GetLeaveRequestForEditAsync_ShouldReturnLeaveRequestViewModel()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var leaveRequestRepository = new Mock<ILeaveRequestRepository>();
            var employeeRepository = new Mock<IEmployeeRepository>();
            var leaveRequestService = new LeaveRequestService(mapper.Object, leaveRequestRepository.Object, employeeRepository.Object);

            var leaveRequestId = 1; // Replace with a valid leave request ID.
            var leaveRequest = new LeaveRequest(); // Replace with a valid LeaveRequest object.

            leaveRequestRepository.Setup(r => r.GetLeaveRequestByIdAsync(leaveRequestId)).ReturnsAsync(leaveRequest);
            mapper.Setup(m => m.Map<LeaveRequestViewModel>(leaveRequest)).Returns(new LeaveRequestViewModel());

            // Act
            var result = await leaveRequestService.GetLeaveRequestForEditAsync(leaveRequestId);

            // Assert
            Assert.NotNull(result);
            leaveRequestRepository.Verify(r => r.GetLeaveRequestByIdAsync(leaveRequestId), Times.Once); // Verify that the repository method was called once.
            mapper.Verify(m => m.Map<LeaveRequestViewModel>(leaveRequest), Times.Once); // Verify that the mapper method was called once.
        }

        [Fact]
        public async Task UpdateLeaveRequestAsync_ShouldUpdateLeaveRequest()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var leaveRequestRepository = new Mock<ILeaveRequestRepository>();
            var employeeRepository = new Mock<IEmployeeRepository>();
            var leaveRequestService = new LeaveRequestService(mapper.Object, leaveRequestRepository.Object, employeeRepository.Object);

            var leaveRequestViewModel = new LeaveRequestViewModel(); // Replace with a valid LeaveRequestViewModel.
            var leaveRequest = new LeaveRequest(); // Replace with a valid LeaveRequest object.

            mapper.Setup(m => m.Map<LeaveRequest>(leaveRequestViewModel)).Returns(leaveRequest);
            leaveRequestRepository.Setup(r => r.UpdateLeaveRequestAsync(leaveRequest)).Returns(Task.CompletedTask);

            // Act
            await leaveRequestService.UpdateLeaveRequestAsync(leaveRequestViewModel);

            // Assert
            // Add assertions as needed to verify the update operation.
            leaveRequestRepository.Verify(r => r.UpdateLeaveRequestAsync(leaveRequest), Times.Once); // Verify that the repository method was called once.
        }

        // Add more test methods for other scenarios and edge cases as needed.
    }
}
