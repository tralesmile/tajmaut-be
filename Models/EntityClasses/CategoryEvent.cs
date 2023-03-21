namespace tajmautAPI.Models.EntityClasses
{
    public class CategoryEvent
    {
        public int CategoryEventId { get; set; }

        public int? RestaurantId { get; set; }

        public string Name { get; set; } = null!;
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        //1-N Relationships
        public List<Event> Events { get; set; }

        //N-1 Relationships
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
