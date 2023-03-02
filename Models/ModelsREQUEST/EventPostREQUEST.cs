namespace tajmautAPI.Models.ModelsREQUEST
{
    public class EventPostREQUEST
    {
        public int RestaurantId { get; set; } = 0;
        public int CategoryEventId { get; set; } = 0;
        public string Name { get; set; } = null;
        public string Description { get; set; } = null;
        public string EventImage { get; set; } = null;
        public DateTime DateTime { get; set; }
    }
}
