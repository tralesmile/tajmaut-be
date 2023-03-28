using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class CategoryREQUEST
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
