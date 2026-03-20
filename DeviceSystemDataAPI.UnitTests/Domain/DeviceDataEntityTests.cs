using Domain.Constants;
using Domain.Entities;

namespace DeviceSystemDataAPI.UnitTests.Domain
{
    public class DeviceDataEntityTests
    {
        [Fact]
        public void CreateDeviceData_ShouldSetAllProperties()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);

            Assert.Equal("Iphone 15", device.Name);
            Assert.Equal("Apple", device.Brand);
            Assert.Equal(Parameters.Available, device.State);
        }

        [Fact]
        public void CreateDeviceData_ShouldSetCreationTime()
        {
            var before = DateTime.Now;
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);
            var after = DateTime.Now;

            Assert.InRange(device.CreationTime, before, after);
        }

        [Fact]
        public void Update_ShouldChangeAllProperties()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);

            var result = device.Update("Iphone 16", "Apple", Parameters.InUse);

            Assert.Equal("Iphone 16", device.Name);
            Assert.Equal("Apple", device.Brand);
            Assert.Equal(Parameters.InUse, device.State);
            Assert.Same(device, result);
        }

        [Fact]
        public void ChangeState_ShouldUpdateState()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);

            device.ChangeState(Parameters.Inactive);

            Assert.Equal(Parameters.Inactive, device.State);
        }

        [Fact]
        public void IsInUse_ShouldReturnTrue_WhenStateIsInUse()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.InUse);

            Assert.True(device.IsInUse);
        }

        [Fact]
        public void IsInUse_ShouldReturnFalse_WhenStateIsAvailable()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Available);

            Assert.False(device.IsInUse);
        }

        [Fact]
        public void IsInUse_ShouldReturnFalse_WhenStateIsInactive()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.Inactive);

            Assert.False(device.IsInUse);
        }

        [Fact]
        public void Update_ShouldThrowInvalidOperationException_WhenDeviceIsInUse()
        {
            var device = DeviceData.CreateDeviceData("Iphone 15", "Apple", Parameters.InUse);

            Assert.Throws<InvalidOperationException>(() =>
                device.Update("Iphone 16", "Apple", Parameters.Available)
            );
        }
    }
}
