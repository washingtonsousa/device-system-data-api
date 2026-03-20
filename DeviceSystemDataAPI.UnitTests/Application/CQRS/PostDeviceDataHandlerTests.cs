using Application.CQRS.Command.PostDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnityOfWork;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class PostDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();
        private readonly Mock<IUnityOfWork> _uowMock = new();

        public PostDeviceDataHandlerTests()
        {
            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);
        }

        [Fact]
        public async Task ShouldAddDeviceAndCommit()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<DeviceData>())).ReturnsAsync(device);

            var handler = new PostDeviceDataHandler(_repositoryMock.Object, _uowMock.Object);
            await handler.Handle(
                new PostDeviceDataCommand { Name = "Iphone 15", Brand = "Apple", State = Parameters.Available },
                CancellationToken.None
            );

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<DeviceData>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
