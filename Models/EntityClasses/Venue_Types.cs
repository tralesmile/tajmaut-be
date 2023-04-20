using System.Text.Json.Serialization;

namespace TajmautMK.Common.Models.EntityClasses
{
    public class Venue_Types
    {
        public int Venue_TypesId { get; set; }
        public string Name { get; set; }

        //1-N Relationships
        [JsonIgnore]
        public List<Venue> Venues { get; set; }

    }
}
