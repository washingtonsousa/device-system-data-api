namespace Domain.Exceptions
{
    public class DeviceValidationException : Exception
    {
        public DeviceValidationException(string message) : base(message) { }
    }
}
