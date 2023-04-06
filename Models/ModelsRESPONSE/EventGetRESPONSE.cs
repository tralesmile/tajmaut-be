namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class EventGetRESPONSE
    {
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public int CategoryEventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string EventImage { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }

        //updates if event is canceled
        public bool isCanceled { get; set; } = false;
        public string StatusEvent { get; set; } = string.Empty;
        public string VenueName { get; set; } = string.Empty;
        public string VenuePhone { get; set; } = string.Empty;
        public string VenueCity { get; set; } = string.Empty;
    }
}
