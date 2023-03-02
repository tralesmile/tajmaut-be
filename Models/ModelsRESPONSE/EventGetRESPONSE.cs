namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class EventGetRESPONSE : Event
    {
        public string StatusEvent { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public string RestaurantPhone { get; set; } = string.Empty;
    }
}
