using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SRH.Application.Services;
using SRH.Application.Contracts.Repositories.dbo;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SGRH._Domain.Entites;
using SGRH._Domain.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace AuthTests
{
    [TestClass]
    public class FloorTest
    {
        private Mock<IFloorRepository> _repoMock;
        private Mock<ILogger<FloorService>> _loggerMock;
        private Mock<IConfiguration> _configMock;
        private FloorService _floorService;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<IFloorRepository>();
            _loggerMock = new Mock<ILogger<FloorService>>();
            _configMock = new Mock<IConfiguration>();

            _floorService = new FloorService(
                _repoMock.Object,
                _loggerMock.Object,
                _configMock.Object
            );
        }

        [TestMethod]
        public async Task GetFloor_ShouldReturnFloors_WhenRepositoryReturnsData()
        {
            // Arrange
            var floors = new List<Floor>
            {
                new Floor { Id = 1, FloorNumber = "1er Piso" },
                new() { Id = 2, FloorNumber = "2do Piso" }
            };

            _repoMock.Setup(r => r.GetAllFloor()).ReturnsAsync(floors);

            // Act
            var result = await _floorService.GetFloor();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count());
        }

        [TestMethod]
        public async Task GetFloor_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAllFloor()).ThrowsAsync(new System.Exception("DB error"));

            // Act
            var result = await _floorService.GetFloor();

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.Message.Contains("Error"));
        }

        [TestMethod]
        public async Task GetFloorById_ShouldReturnNotFound_WhenFloorIsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetFloorById(It.IsAny<int>())).ReturnsAsync((OperationResult<Floor>)null);

            // Act
            var result = await _floorService.GetFloorById(99, null);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No se encontró ningún piso con el ID 99", result.Message);
        }

        [TestMethod]
        public async Task DisableFloor_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange
            var disableDto = new SRH.Application.DTO.dbo.DisableFloorDto
            {
                FloorId = 1,
                DisabledBy = "tester"
            };

            var floor = new OperationResult<Floor>
            {
                Data = new Floor { Id = 1, FloorNumber = "1er Piso" },
                IsSuccess = true
            };

            _repoMock.Setup(r => r.GetFloorById(disableDto.FloorId)).ReturnsAsync(floor);
            _repoMock.Setup(r => r.UpdateFloor(floor)).Returns(Task.CompletedTask);

            // Act
            var result = await _floorService.DisableFloor(disableDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Piso deshabilitado correctamente", result.Message);
        }
    }
}
