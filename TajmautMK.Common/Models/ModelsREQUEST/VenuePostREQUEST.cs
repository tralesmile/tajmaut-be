using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class VenuePostREQUEST
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "The value must be greater than 0.")]
        public int VenueTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "The value must be greater than 0.")]
        public int Venue_CityId { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string VenueImage { get; set; } = null!;

    }
}
