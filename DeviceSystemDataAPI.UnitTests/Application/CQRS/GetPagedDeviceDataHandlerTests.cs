using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using Moq;

namespace DeviceSystemDataAPI.UnitTests.Application.CQRS
{
    public class GetPagedDeviceDataHandlerTests
    {
        private readonly Mock<IDeviceDataRepository> _repositoryMock = new();

        [Fact]
        public async Task ShouldReturnPagedResult()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            var pagedResult = new PagedResult<DeviceData>(new List<DeviceData> { device }, 1, 1, 4);
            _repositoryMock.Setup(r => r.GetPagedAsync(1, 4, null, null)).ReturnsAsync(pagedResult);

            var handler = new GetPagedDeviceDataHandler(_repositoryMock.Object);
            var result = await handler.Handle(new GetPagedDeviceDataQuery { PageNumber = 1, PageSize = 4 }, CancellationToken.None);

            Assert.Single(result.Items);
            Assert.Equal(1, result.Total);
        }
    }
}
