using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class CategoryREQUEST
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
