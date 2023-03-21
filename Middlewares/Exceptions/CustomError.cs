using System.Runtime.InteropServices;

namespace tajmautAPI.Middlewares.Exceptions
{
    public class CustomError : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomError(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
