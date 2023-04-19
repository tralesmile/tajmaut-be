using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Common.Models.EntityClasses
{
    public class Venue
    {

        public int VenueId { get; set; }

        [Required]
        public int VenueTypeId { get; set; }

        [Required]
        public int Venue_CityId { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public string VenueImage { get; set; } = null!;

        [Required]
        public DateTime ModifiedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        //N-1 Relationships
        [JsonIgnore]
        public virtual Venue_Types VenueType { get; set; }

        [JsonIgnore]
        public virtual Venue_City Venue_City { get; set; }

        //1-N Relationships
        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; }

        [JsonIgnore]
        public List<OnlineReservation> OnlineReservations { get; set; }

    }
}
