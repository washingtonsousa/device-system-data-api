using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DeviceDataRepository : IDeviceDataRepository
    {    
        
        private readonly DatabaseContext _context;
        public DeviceDataRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DeviceData> GetById(string id)
        {
            return await _context.DeviceData.FirstOrDefaultAsync((device) =>  device.Id == id );
        }

        public async Task<PagedResult<DeviceData>> GetPaged(int page = 1, int offset = 4, string brand = null, string state = null)
        {
            var queryable = _context.DeviceData.AsQueryable();

            if(!string.IsNullOrEmpty(brand))
            {
                queryable = queryable.Where((device) => device.Brand == brand);
            }

            if (!string.IsNullOrEmpty(state))
            {
                queryable = queryable.Where((device) => device.State == state);
            }

            return new PagedResult<DeviceData>(

                await queryable.Skip((page - 1) * offset).Take(offset).ToListAsync(),
                page,
                await queryable.CountAsync(),
                offset
            );
        }
    }
}
