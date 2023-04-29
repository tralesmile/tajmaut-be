using System.Text.Json.Serialization;

namespace TajmautMK.Common.Models.EntityClasses
{
    public class ForgotPassEntity
    {
        public int ForgotPassEntityId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }

        //N-1 Relationships
        //escape serialization
        [JsonIgnore]
        public virtual User user { get; set; } = null!;
    }
}
