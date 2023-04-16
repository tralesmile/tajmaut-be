using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.ModelsRESPONSE;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class EventFilterRESPONSE : BaseFilterRESPONSE
    {
        public List<EventGetRESPONSE> Events { get; set; } = new List<EventGetRESPONSE>();
    }
}
