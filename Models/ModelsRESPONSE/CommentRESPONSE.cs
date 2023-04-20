namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class CommentRESPONSE
    {
        public int CommentId { get; set; }

        public int VenueId { get; set; }

        public int UserId { get; set; }

        public string Body { get; set; } = null!;

        public int Review { get; set; }

        public DateTime DateTime { get; set; }

    }
}
