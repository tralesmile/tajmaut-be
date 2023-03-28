namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class TokenRESPONSE
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
