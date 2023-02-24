namespace tajmautAPI.Models
{

    //get more info about the events
    public class EventGET : Event
    {
        public string StatusEvent { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public string RestaurantPhone { get; set; }= string.Empty;
    }
}
