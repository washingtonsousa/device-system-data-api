using Application.CQRS.Command.GetDeviceData.GetPagedDeviceData;
using Application.CQRS.Command.PatchDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class PatchDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();
        private readonly Mock<IUnityOfWork> _uowMock = new();

        public PatchDeviceDataHandlerTests()
        {
            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task ShouldChangeStateAndCommit()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            _repositoryMock.Setup(r => r.GetByIdAsync("123", false)).ReturnsAsync(device);

            var handler = new PatchDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);
            var result = await handler.Handle(
                new PatchDeviceDataCommand { DeviceId = "123", State = Parameters.InUse },
                CancellationToken.None
            );

            Assert.Equal(Parameters.InUse, result.State);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowDeviceNotFoundException_WhenDeviceDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("999", false)).ReturnsAsync((DeviceData?)null);

            var handler = new PatchDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);

            await Assert.ThrowsAsync<DeviceNotFoundException>(() =>
                handler.Handle(
                    new PatchDeviceDataCommand { DeviceId = "999", State = Parameters.InUse },
                    CancellationToken.None
                )
            );
        }
    }
}
