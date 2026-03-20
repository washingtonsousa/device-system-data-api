using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class DeviceDataRepository : IDeviceDataRepository
    {    
        
        private readonly DatabaseContext _context;
        public DeviceDataRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DeviceData> AddAsync(DeviceData deviceData) => _context.DeviceData.Add(deviceData).Entity;
        public async Task<DeviceData> GetByIdAsync(string id, bool asNoTracking = true)
        {
            var queryable = _context.DeviceData.AsQueryable();

            if(asNoTracking)
              queryable = queryable.AsNoTracking();

            return await queryable.FirstOrDefaultAsync((device) =>  device.Id == id );
        }

        public async Task<PagedResult<DeviceData>> GetPagedAsync(int page = 1, int offset = 4, string brand = null, string state = null)
        {
            var queryable = _context.DeviceData.AsNoTracking().AsQueryable();

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

        public void RemoveAsync(DeviceData device) => _context.DeviceData.Remove(device);

    }
}
