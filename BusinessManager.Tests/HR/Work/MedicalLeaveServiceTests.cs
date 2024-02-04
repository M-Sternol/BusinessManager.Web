using System;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.Services.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Moq;
using Xunit;

namespace BusinessManager.Tests.HR.Employee
{
    public class MedicalLeaveServiceTests
    {
        [Fact]
        public async Task GetMedicalLeaveByIdAsync_ValidId_ReturnsMedicalLeaveViewModel()
        {
            // Arrange
            var medicalLeaveId = 1;
            var expectedMedicalLeaveViewModel = new MedicalLeaveViewModel { /* Inicjalizuj oczekiwany widok modelu */ };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<MedicalLeaveViewModel>(It.IsAny<MedicalLeave>()))
                .Returns(expectedMedicalLeaveViewModel);

            var mockMedicalLeaveRepository = new Mock<IMedicalLeaveRepository>();
            mockMedicalLeaveRepository.Setup(repo => repo.GetMedicalLeaveByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new MedicalLeave()); // Zwróć prawidłowe zwolnienie lekarskie

            var medicalLeaveService = new MedicalLeaveService(mockMedicalLeaveRepository.Object, mockMapper.Object);

            // Act
            var result = await medicalLeaveService.GetMedicalLeaveByIdAsync(medicalLeaveId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MedicalLeaveViewModel>(result);
        }

        [Fact]
        public async Task CreateMedicalLeaveAsync_ValidModel_ReturnsMedicalLeaveId()
        {
            // Arrange
            var medicalLeaveViewModel = new MedicalLeaveViewModel { /* Inicjalizuj model zwolnienia lekarskiego */ };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<MedicalLeave>(It.IsAny<MedicalLeaveViewModel>()))
                .Returns(new MedicalLeave()); // Zwróć prawidłowe zwolnienie lekarskie

            var mockMedicalLeaveRepository = new Mock<IMedicalLeaveRepository>();
            mockMedicalLeaveRepository.Setup(repo => repo.AddMedicalLeaveAsync(It.IsAny<MedicalLeave>()))
                .ReturnsAsync(1); // Zakładając, że 1 to ID nowego zwolnienia lekarskiego

            var medicalLeaveService = new MedicalLeaveService(mockMedicalLeaveRepository.Object, mockMapper.Object);

            // Act
            var result = await medicalLeaveService.CreateMedicalLeaveAsync(medicalLeaveViewModel);

            // Assert
            Assert.Equal(1, result); // Upewnij się, że zwrócono poprawne ID zwolnienia lekarskiego
        }

        [Fact]
        public async Task UpdateMedicalLeaveAsync_ValidModel_DoesNotThrowException()
        {
            // Arrange
            var medicalLeaveViewModel = new MedicalLeaveViewModel { /* Inicjalizuj model zwolnienia lekarskiego */ };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<MedicalLeave>(It.IsAny<MedicalLeaveViewModel>()))
                .Returns(new MedicalLeave()); // Zwróć prawidłowe zwolnienie lekarskie

            var mockMedicalLeaveRepository = new Mock<IMedicalLeaveRepository>();
            mockMedicalLeaveRepository.Setup(repo => repo.UpdateMedicalLeavesAsync(It.IsAny<MedicalLeave>()))
                .Returns(Task.CompletedTask); // Symuluj poprawną aktualizację

            var medicalLeaveService = new MedicalLeaveService(mockMedicalLeaveRepository.Object, mockMapper.Object);

            // Act & Assert
            try
            {
                await medicalLeaveService.UpdateMedicalLeaveAsync(medicalLeaveViewModel);
            }
            catch (Exception ex)
            {
                // Jeśli zostanie zgłoszony wyjątek, test nie przejdzie
                Assert.True(false, $"Złapany nieoczekiwany wyjątek: {ex}");
            }

            // Jeśli nie zostanie zgłoszony wyjątek, test przejdzie pomyślnie
            Assert.True(true);
        }


        [Fact]
        public async Task DeleteMedicalLeaveAsync_ValidMedicalLeaveId_ReturnsTrue()
        {
            // Arrange
            var medicalLeaveId = 1;

            var mockMedicalLeaveRepository = new Mock<IMedicalLeaveRepository>();
            mockMedicalLeaveRepository.Setup(repo => repo.GetMedicalLeaveByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new MedicalLeave()); // Zwróć prawidłowe zwolnienie lekarskie
            mockMedicalLeaveRepository.Setup(repo => repo.DeleteMedicalleavesAsync(It.IsAny<MedicalLeave>()))
                .Returns(Task.CompletedTask); // Symuluj poprawne usunięcie

            var medicalLeaveService = new MedicalLeaveService(mockMedicalLeaveRepository.Object, It.IsAny<IMapper>());

            // Act
            var result = await medicalLeaveService.DeleteMedicalLeaveAsync(medicalLeaveId);

            // Assert
            Assert.True(result); // Upewnij się, że zwrócono true dla poprawnego usuwania
        }
    }
}
