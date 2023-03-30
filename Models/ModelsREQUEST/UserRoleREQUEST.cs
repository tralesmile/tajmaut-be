using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class UserRoleREQUEST
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(User|Manager|Admin)$", ErrorMessage = "Role must be User, Manager, or Admin")]
        public string Role { get; set; }
    }
}
