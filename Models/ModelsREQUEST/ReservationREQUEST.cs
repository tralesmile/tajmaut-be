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
        public string FullName { get; set; } = null;
    }
}
