using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
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

        [Required]
        [RegularExpression(@"^(1\d{2}|[1-9]\d|\b[1-9]\b)$", ErrorMessage = "The duration of the event must be between 1 and 200 hours.")]
        public int Duration { get; set; }
    }
}
