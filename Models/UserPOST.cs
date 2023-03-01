using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models
{
    public class UserPOST
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

    }
}
