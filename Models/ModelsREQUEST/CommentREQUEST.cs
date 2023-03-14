using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class CommentREQUEST
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Body { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5")]
        [Required]
        public int Review { get; set; }
    }
}
