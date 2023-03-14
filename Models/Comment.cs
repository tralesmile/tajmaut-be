using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tajmautAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Body { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5")]
        [Required]
        public int Review { get; set; }

        //N-1 Relationships
        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
