namespace tajmautAPI.Exceptions
{
    public class CustomBadRequestException : Exception
    {

        public CustomBadRequestException()
        {

        }

        public CustomBadRequestException(string message) : base(message)
        {
        }

        public CustomBadRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CustomBadRequestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
