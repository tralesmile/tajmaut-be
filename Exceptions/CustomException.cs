using System.Net;
using System.Net.NetworkInformation;

namespace tajmautAPI.Exceptions
{
    public class CustomException : Exception
    {

        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode,string message)
        {
            Message= message;
            StatusCode = statusCode;
        }

    }
}
