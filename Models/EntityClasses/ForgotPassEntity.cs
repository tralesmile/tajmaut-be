﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using tajmautAPI.Models.EntityClasses;

namespace TajmautMK.Common.Models.EntityClasses
{
    public class ForgotPassEntity
    {
        public int ForgotPassEntityId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }

        //N-1 Relationships
        //escape serialization
        [JsonIgnore]
        public virtual User user { get; set; } = null!;
    }
}
