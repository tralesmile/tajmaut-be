namespace tajmautAPI.Services.Implementations
{
    public class ServiceResponse<T>
    {
        public bool isError { get; set; } = false;
        public int statusCode { get; set; }
        public string? errorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
