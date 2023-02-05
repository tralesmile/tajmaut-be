namespace tajmautAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; }= null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;

        //1-N Relationships
        public List<Comment> Comments { get; set; }
        public List<OnlineReservation> OnlineReservations { get; set; }

    }
}
