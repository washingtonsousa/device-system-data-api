using Domain.Entities;
using Domain.Entities.Base;

namespace Domain.Repositories
{
    public interface IDeviceDataRepository
    {
        public Task<PagedResult<DeviceData>> GetPaged(int page = 1, int offset = 4, string brand = null, string state = null);
        public Task<DeviceData> GetById(string id);

    }
}
