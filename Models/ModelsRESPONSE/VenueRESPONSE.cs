namespace tajmautAPI.Models.ModelsRESPONSE
{
    public class VenueRESPONSE
    {
        public int VenueId { get; set; }

        public int VenueTypeId { get; set; }

        public int Venue_CityId { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int ManagerId { get; set; }

    }
}
