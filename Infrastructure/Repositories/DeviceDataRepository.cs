using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DeviceDataRepository : IDeviceDataRepository
    {    


        public Task<DeviceData> Get(string brand, string name)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceData> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<DeviceData>> GetPaged()
        {
            throw new NotImplementedException();
        }
    }
}
