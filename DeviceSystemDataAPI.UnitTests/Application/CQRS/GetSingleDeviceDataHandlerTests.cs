using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Application.CQRS.Query.GetDeviceData.GetSingleDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class GetSingleDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();

        [Fact]
        public async Task ShouldReturnDevice_WhenDeviceExists()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", true)).ReturnsAsync(device);

            var handler = new GetSingleDeviceDataHandler(_repositoryMock.Object);
            var result = await handler.Handle(new GetDeviceDataQuery { DeviceId = "123" }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Iphone 15", result.Name);
        }

        [Fact]
        public async Task ShouldReturnNull_WhenDeviceDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("999", true)).ReturnsAsync((DeviceData?)null);

            var handler = new GetSingleDeviceDataHandler(_repositoryMock.Object);
            var result = await handler.Handle(new GetDeviceDataQuery { DeviceId = "999" }, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
