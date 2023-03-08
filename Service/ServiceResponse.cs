using System.Net;

namespace tajmautAPI.Service
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
    }
}
