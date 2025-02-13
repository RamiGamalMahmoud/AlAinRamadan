namespace AlAinRamadan.Core
{
    public class AppNitification
    {
        public AppNitification(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }

    public class SuccessNotification : AppNitification
    {
        public SuccessNotification(string message) : base(message)
        {
        }
    }

    public class ErrorNotification : AppNitification
    {
        public ErrorNotification(string message) : base(message)
        {
        }
    }
}
