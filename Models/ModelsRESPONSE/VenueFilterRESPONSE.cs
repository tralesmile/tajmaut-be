namespace TajmautMK.Common.Models.ModelsRESPONSE
{
    public class VenueFilterRESPONSE : BaseFilterRESPONSE
    {
        public List<VenueRESPONSE> Venues { get; set; } = new List<VenueRESPONSE>();
    }
}
