using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class ReservationREQUEST
    {

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        [Required]
        public int NumberGuests { get; set; } = 0;

        [Required]
        public string Phone { get; set; } = null;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
