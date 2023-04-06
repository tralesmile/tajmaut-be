using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using tajmautAPI.Models.EntityClasses;

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
