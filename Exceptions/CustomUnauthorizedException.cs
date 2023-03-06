namespace tajmautAPI.Exceptions
{
    public class CustomUnauthorizedException : Exception
    {
        public CustomUnauthorizedException()
        {

        }

        public CustomUnauthorizedException(string message) : base(message)
        {
        }

        public CustomUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CustomUnauthorizedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
