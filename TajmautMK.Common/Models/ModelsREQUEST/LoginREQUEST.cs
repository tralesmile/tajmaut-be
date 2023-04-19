
using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class LoginREQUEST
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
