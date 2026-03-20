using Domain.Entities;
using Domain.Entities.Base;

namespace Domain.Repositories
{
    public interface IDeviceDataRepository
    {
        public Task<PagedResult<DeviceData>> GetPaged();
        public Task<DeviceData> GetById(Guid id);
        public Task<DeviceData> Get(string brand, string name);

    }
}
