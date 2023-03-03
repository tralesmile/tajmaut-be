using System.Net;

namespace tajmautAPI.Exceptions
{
    public class ExceptionStatusCodes
    {
        public static Dictionary<Type, HttpStatusCode> exceptionStatusCodes = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(CustomNotFoundException), HttpStatusCode.NotFound },
            { typeof(CustomBadRequestException), HttpStatusCode.BadRequest },
        };

        public static HttpStatusCode GetExceptionStatusCode(Exception exception)
        {
            bool exceptionFound = exceptionStatusCodes.TryGetValue(exception.GetType(), out var statusCode);
            return exceptionFound ? statusCode : HttpStatusCode.InternalServerError;
        }

    }
}
