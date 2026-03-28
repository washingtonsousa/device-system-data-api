using Application.CQRS.Command.DeleteDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class DeleteDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();
        private readonly Mock<IUnityOfWork> _uowMock = new();

        public DeleteDeviceDataHandlerTests()
        {
            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task ShouldDeleteDeviceAndCommit()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", false)).ReturnsAsync(device);

            var handler = new DeleteDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);
            await handler.Handle(new DeleteDeviceDataCommand { DeviceId = "123" }, CancellationToken.None);

            _repositoryMock.Verify(r => r.RemoveAsync(device), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("999", false)).ReturnsAsync((DeviceData?)null);

            var handler = new DeleteDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);

            await Assert.ThrowsAsync<DeviceNotFoundException>(() =>
                handler.Handle(new DeleteDeviceDataCommand { DeviceId = "999" }, CancellationToken.None)
            );
        }

        [Fact]
        public async Task ShouldThrowDeviceStateConflictException_WhenDeviceIsInUse()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.InUse);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", false)).ReturnsAsync(device);

            var handler = new DeleteDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);

            await Assert.ThrowsAsync<DeviceStateConflictException>(() =>
                handler.Handle(new DeleteDeviceDataCommand { DeviceId = "123" }, CancellationToken.None)
            );
        }
    }
}
