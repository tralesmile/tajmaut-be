using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class BaseFilterRESPONSE
    {
        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }
    }
}
