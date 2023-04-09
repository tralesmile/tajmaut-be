using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class BaseFilterREQUEST
    {
        public int? PageNumber { get; set; } = null;

        public int? ItemsPerPage { get; set; } = null;
    }
}
