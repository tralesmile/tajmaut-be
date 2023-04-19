using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class UserPutREQUEST
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
    }
}
