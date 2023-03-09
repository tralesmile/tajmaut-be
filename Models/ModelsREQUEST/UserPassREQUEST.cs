using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class UserPassREQUEST
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set;}
    }
}
