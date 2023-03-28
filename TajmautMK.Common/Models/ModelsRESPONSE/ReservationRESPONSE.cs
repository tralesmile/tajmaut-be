using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class ReservationRESPONSE
    {
        public int OnlineReservationId { get; set; }

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public int NumberGuests { get; set; }

        public string Phone { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
