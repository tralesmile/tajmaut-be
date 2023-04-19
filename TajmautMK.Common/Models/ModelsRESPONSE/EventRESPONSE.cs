namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class EventRESPONSE
    {
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public int CategoryEventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string EventImage { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
    }
}
