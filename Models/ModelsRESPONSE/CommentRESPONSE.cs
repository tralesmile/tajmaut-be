namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class CommentRESPONSE
    {
        public int CommentId { get; set; }

        public int RestaurantId { get; set; }

        public int UserId { get; set; }

        public string Body { get; set; } = null!;

        public int Review { get; set; }

        public DateTime DateTime { get; set; }

    }
}
