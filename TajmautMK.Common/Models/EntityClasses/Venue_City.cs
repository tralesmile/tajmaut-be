using System.Text.Json.Serialization;

namespace TajmautMK.Common.Models.EntityClasses
{
    public class Venue_City
    {
        public int Venue_CityId { get; set; }
        public string CityName { get; set; }

        //1-N Relationships
        [JsonIgnore]
        public List<Venue> Venues { get; set; }
    }
}
