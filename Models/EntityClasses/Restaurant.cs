using System.Text.Json.Serialization;

namespace tajmautAPI.Models.EntityClasses
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Phone { get; set; } = null!;


        //1-N Relationships

        public List<Comment> Comments { get; set; }
        public List<Event> Events { get; set; }
        public List<CategoryEvent> CategoryEvents { get; set; }
        public List<OnlineReservation> OnlineReservations { get; set; }




    }
}
