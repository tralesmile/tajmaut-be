using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tajmautAPI.Models.EntityClasses
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public DateTime ModifiedAt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        //1-N Relationships
        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; }

        [JsonIgnore]
        public List<OnlineReservation> OnlineReservations { get; set; }




    }
}
