using Application.CQRS.Command.PutDeviceData;
using Application.CQRS.Command.PutDeviceData.GetPagedDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class PutDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();
        private readonly Mock<IUnityOfWork> _uowMock = new();

        public PutDeviceDataHandlerTests()
        {
            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task ShouldUpdateDeviceAndCommit()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", false)).ReturnsAsync(device);

            var handler = new PutDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);
            var result = await handler.Handle(
                new PutDeviceDataCommand
                {
                    DeviceId = "123",
                    Name = "Iphone 16",
                    Brand = "Apple",
                    State = Parameters.InUse
                },
                CancellationToken.None
            );

            Assert.Equal("Iphone 16", result.Name);
            Assert.Equal("Apple", result.Brand);
            Assert.Equal(Parameters.InUse, result.State);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("999", false)).ReturnsAsync((DeviceData?)null);

            var handler = new PutDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);

            await Assert.ThrowsAsync<DeviceNotFoundException>(() =>
                handler.Handle(
                    new PutDeviceDataCommand
                    {
                        DeviceId = "999",
                        Name = "Iphone 15",
                        Brand = "Apple",
                        State = Parameters.Available
                    },
                    CancellationToken.None
                )
            );
        }

        [Fact]
        public async Task ShouldThrowDeviceStateConflictException_WhenDeviceIsInUse()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.InUse);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", false)).ReturnsAsync(device);

            var handler = new PutDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);

            await Assert.ThrowsAsync<DeviceStateConflictException>(() =>
                handler.Handle(
                    new PutDeviceDataCommand
                    {
                        DeviceId = "123",
                        Name = "Iphone 16",
                        Brand = "Apple",
                        State = Parameters.Available
                    },
                    CancellationToken.None
                )
            );
        }
    }
}
