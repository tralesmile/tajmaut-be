using System.Text.Json.Serialization;

namespace tajmautAPI.Models
{
    public class OnlineReservation
    {
        public int OnlineReservationId { get; set; }

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public int NumberGuests { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        //N-1 Relationship

        public virtual Event Event { get; set; } = null!;

        public virtual Restaurant Restaurant { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
