namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class UserRESPONSE
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Role { get; set; } = null;
    }
}
