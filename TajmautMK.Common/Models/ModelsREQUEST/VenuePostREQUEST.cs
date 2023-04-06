using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace tajmautAPI.Models.ModelsREQUEST
{
    public class VenuePostREQUEST
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int VenueTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Venue_CityId { get; set; }

        [Required]
        public string Phone { get; set; }

    }
}
