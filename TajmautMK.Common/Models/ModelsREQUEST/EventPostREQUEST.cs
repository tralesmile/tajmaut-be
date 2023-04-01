using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class EventPostREQUEST
    {
        [Required]
        public int VenueId { get; set; } = 0;
        [Required]
        public int CategoryEventId { get; set; } = 0;
        [Required]
        public string Name { get; set; } = null;
        [Required]
        public string Description { get; set; } = null;
        [Required]
        public string EventImage { get; set; } = null;
        [Required]
        public DateTime DateTime { get; set; }
    }
}
