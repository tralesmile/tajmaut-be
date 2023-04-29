using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class CommentREQUEST
    {
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "The value must be greater than 0.")]
        public int VenueId { get; set; }

        [Required]
        public string Body { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5")]
        [Required]
        public int Review { get; set; }
    }
}
