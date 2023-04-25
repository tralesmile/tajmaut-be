using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class ReservationRESPONSE
    {
        public int OnlineReservationId { get; set; }

        public int VenueId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public int NumberGuests { get; set; }

        public string Phone { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
