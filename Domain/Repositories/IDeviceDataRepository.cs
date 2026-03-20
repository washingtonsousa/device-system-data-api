using Domain.Entities;
using Domain.Entities.Base;

namespace Domain.Repositories
{
    public interface IDeviceDataRepository
    {
        public Task<PagedResult<DeviceData>> GetPagedAsync(int page = 1, int offset = 4, string brand = null, string state = null);
        public Task<DeviceData> GetByIdAsync(string id, bool asNoTracking = true);

        public Task<DeviceData> AddAsync(DeviceData deviceData);
        public void RemoveAsync(DeviceData device);
    }
}
