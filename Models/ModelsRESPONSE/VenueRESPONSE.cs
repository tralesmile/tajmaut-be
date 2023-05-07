using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsRESPONSE;

namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class VenueRESPONSE
    {
        public int VenueId { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int ManagerId { get; set; }

        public string VenueImage { get; set; } = null!;

        public string VenueTypeName { get; set; } = null!;

        public Venue_Types VenueType { get; set; }

        public Venue_City VenueCity { get; set; }

        public string WorkingHours { get; set; }

        public Location Location { get; set; }

        public GalleryImages? GalleryImages { get; set; }

    }
}
