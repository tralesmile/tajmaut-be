using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Common.Models.ModelsREQUEST
{
    public class VenuePutREQUEST
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

        [Required]
        public string WorkingHours { get; set; }

        [Required]
        public Location Location { get; set; }

        public GalleryImages? GalleryImages { get; set; }

    }
}
