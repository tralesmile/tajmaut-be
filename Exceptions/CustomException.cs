using System.Net;
using System.Net.NetworkInformation;

namespace tajmautAPI.Exceptions
{
    public class CustomException : Exception
    {

        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode,string message)
        {
            Message= message;
            StatusCode = statusCode;
        }

    }
}
