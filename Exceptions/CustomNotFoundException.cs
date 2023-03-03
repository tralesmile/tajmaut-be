namespace tajmautAPI.Exceptions
{
    public class CustomNotFoundException : Exception
    {

        public CustomNotFoundException()
        {

        }

        public CustomNotFoundException(string message) : base(message)
        {
        }

        public CustomNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CustomNotFoundException(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
