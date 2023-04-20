using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class UserPassREQUEST
    {
        [Required]
        public string OldPassword { get; set; }

        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
        public string NewPassword { get; set; }

        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
        public string ConfirmPassword { get; set;}
    }
}
