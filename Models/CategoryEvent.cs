namespace tajmautAPI.Models
{
    public class CategoryEvent
    {
        public int CategoryEventId { get; set; }

        public int RestaurantId { get; set; }

        public string Name { get; set; } = null!;

        //1-N Relationships
        public List<Event> Events { get; set; }

        //N-1 Relationships
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
