using System.Text.Json.Serialization;

namespace tajmautAPI.Models.EntityClasses
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
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        //updates if event is canceled
        public bool isCanceled { get; set; } = false;

        //N-1 Relationships
        //escape serialization
        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; } = null!;

        //escape serialization
        [JsonIgnore]
        public virtual CategoryEvent CategoryEvent { get; set; } = null!;

        //1-N Relationships
        [JsonIgnore]
        public List<OnlineReservation> OnlineReservations { get; set; }


    }
}
