namespace Domain.Exceptions
{
    public class DeviceStateConflictException : Exception
    {
        public DeviceStateConflictException(string message) : base(message) { }
    }
}
