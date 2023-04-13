using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class EventFilterREQUEST : BaseFilterREQUEST
    {
        public int? CategoryId { get; set; } = null;

        public int? CityId { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set;} = null;
    }
}
