using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class ReservationREQUEST
    {
        [Required]
        public int VenueId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "The number of guests must be between 1 and 50.")]
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
