using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class MailSendREQUEST
    {
        public string? To { get; set; } = null;

        public string? From { get; set; } = null;

        public string? Template { get; set; } = null;

        public string?  Subject { get; set; } = null;
    }
}
