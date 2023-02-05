using System.Text.Json.Serialization;

namespace tajmautAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public string Body { get; set; } = null!;

        public int Review { get; set; }

        //N-1 Relationships
        public virtual Restaurant Restaurant { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
