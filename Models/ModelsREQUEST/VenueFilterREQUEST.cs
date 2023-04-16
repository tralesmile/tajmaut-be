using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class VenueFilterREQUEST : BaseFilterREQUEST
    {
        public int? CityId { get; set; } = null;

        public int? VenueTypeId { get; set; } = null;

    }
}
