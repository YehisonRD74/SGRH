using SGRH.Application.DTO.dbo;
using Xunit;

namespace SGRH.Tests
{
    public class FloorRepositoryTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var expected = 1;

            // Act
            var actual = expected;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateFloor_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateFloorDto
            {
                FloorId = 1,
                FloorNumber = 2
            };

            // Act
            var result = await _repository.CreateFloor(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(dto.FloorNumber, result.Data.FloorNumber);
        }

        [Fact]
        public async Task CreateFloor_WithNullData_ReturnsFailure()
        {
            // Act
            var result = await _repository.CreateFloor(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Datos inválidos", result.Message);
        }
    }
}

