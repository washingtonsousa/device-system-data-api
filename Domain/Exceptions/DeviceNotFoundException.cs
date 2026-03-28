namespace Domain.Exceptions
{
    public class DeviceNotFoundException : Exception
    {
        public DeviceNotFoundException(string deviceId)
            : base($"Device '{deviceId}' not found.") { }
    }
}
