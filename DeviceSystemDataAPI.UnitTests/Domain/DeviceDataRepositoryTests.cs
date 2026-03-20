using Domain.Constants;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeviceSystemDataAPI.UnitTests.Domain
{
    public class DeviceDataRepositoryTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DeviceDataRepository _repository;

        public DeviceDataRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(options);
            _repository = new DeviceDataRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task<DeviceData> SeedDevice(string name = "Iphone 15", string brand = "Apple", string state = "available")
        {
            var device = DeviceData.CreateDeviceData(name, brand, state);
            _context.DeviceData.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDevice_WhenDeviceExists()
        {
            var seeded = await SeedDevice();

            var result = await _repository.GetByIdAsync(seeded.Id);

            Assert.NotNull(result);
            Assert.Equal(seeded.Id, result.Id);
            Assert.Equal("Iphone 15", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenDeviceDoesNotExist()
        {
            var result = await _repository.GetByIdAsync("invalid-id");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResult()
        {
            await SeedDevice("Iphone 13", "Apple", Parameters.Available);
            await SeedDevice("Iphone 15", "Apple", Parameters.InUse);
            await SeedDevice("Iphone 16", "Apple", Parameters.Inactive);

            var result = await _repository.GetPagedAsync(page: 1, offset: 2);

            Assert.Equal(2, result.Items.Count);
            Assert.Equal(3, result.Total);
            Assert.Equal(1, result.Page);
            Assert.Equal(2, result.ItemsPerPage);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldFilterByBrand()
        {
            await SeedDevice("Iphone 13", "Apple", Parameters.Available);
            await SeedDevice("Galaxy S24", "Samsung", Parameters.Available);
            await SeedDevice("Iphone 16", "Apple", Parameters.InUse);

            var result = await _repository.GetPagedAsync(brand: "Apple");

            Assert.Equal(2, result.Items.Count);
            Assert.All(result.Items, d => Assert.Equal("Apple", d.Brand));
        }

        [Fact]
        public async Task GetPagedAsync_ShouldFilterByState()
        {
            await SeedDevice("Iphone 13", "Apple", Parameters.Available);
            await SeedDevice("Iphone 15", "Apple", Parameters.InUse);
            await SeedDevice("Iphone 16", "Apple", Parameters.Available);

            var result = await _repository.GetPagedAsync(state: Parameters.Available);

            Assert.Equal(2, result.Items.Count);
            Assert.All(result.Items, d => Assert.Equal(Parameters.Available, d.State));
        }

        [Fact]
        public async Task GetPagedAsync_ShouldFilterByBrandAndState()
        {
            await SeedDevice("Iphone 13", "Apple", Parameters.Available);
            await SeedDevice("Galaxy S24", "Samsung", Parameters.InUse);
            await SeedDevice("Iphone 16", "Apple", Parameters.InUse);

            var result = await _repository.GetPagedAsync(brand: "Apple", state: Parameters.Available);

            Assert.Single(result.Items);
            Assert.Equal("Apple", result.Items[0].Brand);
            Assert.Equal(Parameters.Available, result.Items[0].State);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnEmpty_WhenNoDevicesMatch()
        {
            await SeedDevice("Iphone 13", "Apple", Parameters.Available);

            var result = await _repository.GetPagedAsync(brand: "NonExistent");

            Assert.Empty(result.Items);
            Assert.Equal(0, result.Total);
        }

        [Fact]
        public async Task AddAsync_ShouldAddDeviceToContext()
        {
            var device = DeviceData.CreateDeviceData("Redmi Note", "Xiaomi", Parameters.Available);

            await _repository.AddAsync(device);
            await _context.SaveChangesAsync();

            var saved = await _context.DeviceData.FirstOrDefaultAsync(d => d.Name == "Redmi Note");
            Assert.NotNull(saved);
            Assert.Equal("Xiaomi", saved.Brand);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveDeviceFromContext()
        {
            var seeded = await SeedDevice();

            _repository.RemoveAsync(seeded);
            await _context.SaveChangesAsync();

            var result = await _context.DeviceData.FirstOrDefaultAsync(d => d.Id == seeded.Id);
            Assert.Null(result);
        }
    }
}
