using Microsoft.AspNetCore.SignalR;

namespace tajmautAPI.Models.EntityClasses
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        //public string Password { get; set; } = null!;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        //default user registration
        public string Role { get; set; } = "User";
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }

        //1-N Relationships
        public List<Comment> Comments { get; set; }
        public List<OnlineReservation> OnlineReservations { get; set; }

    }
}
