using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.ModelsRESPONSE;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class VenueFilterRESPONSE : BaseFilterRESPONSE
    {
        public List<VenueRESPONSE> Venues { get; set; } = new List<VenueRESPONSE>();
    }
}
