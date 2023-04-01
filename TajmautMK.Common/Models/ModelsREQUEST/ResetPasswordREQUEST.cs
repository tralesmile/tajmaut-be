﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class ResetPasswordREQUEST
    {

        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
