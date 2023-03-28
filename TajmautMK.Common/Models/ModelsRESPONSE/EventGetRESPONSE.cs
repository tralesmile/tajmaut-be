namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class EventGetRESPONSE
    {
        public int EventId { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryEventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string EventImage { get; set; } = null!;
        public DateTime DateTime { get; set; }

        //updates if event is canceled
        public bool isCanceled { get; set; } = false;
        public string StatusEvent { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public string RestaurantPhone { get; set; } = string.Empty;
    }
}
