using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tajmautAPI.Models.EntityClasses
{
    public class OnlineReservation
    {
        public int OnlineReservationId { get; set; }

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        [Required]
        public int NumberGuests { get; set; }

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;


        public DateTime ModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public bool IsActive { get; set; } = false;

        //N-1 Relationship

        [JsonIgnore]
        public virtual Event Event { get; set; } = null!;

        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; } = null!;

        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
