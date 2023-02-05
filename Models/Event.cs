namespace tajmautAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public int RestaurantId { get; set; }

        public int CategoryEventId { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string EventImage { get; set; } = null!;


        public DateTime DateTime { get; set; }
        //N-1 Relationships
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual CategoryEvent CategoryEvent { get; set; } = null!;
        //1-N Relationships
        public List<OnlineReservation> OnlineReservations { get; set; }


    }
}
