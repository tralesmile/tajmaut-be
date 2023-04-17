using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class FilterRESPONSE<T> : BaseFilterRESPONSE
    {
        public List<T> Items { get; set; }
    }
}
