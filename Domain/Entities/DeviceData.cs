using Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DeviceData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Brand { get; private set; }

        public string State { get; private set; }

        public DateTime CreationTime { get; private set; }

        [NotMapped]
        public bool IsInUse {get {
                return State == Parameters.InUse;
            } }

        private DeviceData() { }

        public static DeviceData CreateDeviceData(string name, string brand, string state)
        {
            return new DeviceData() {
                Name = name,
                Brand = brand,
                State = state,
                CreationTime = DateTime.Now
            };
        }

        public DeviceData Update(string name, string brand, string state)
        {

            Name = name;
            Brand = brand;
            State = state;

            return this;
        }


    }
}
