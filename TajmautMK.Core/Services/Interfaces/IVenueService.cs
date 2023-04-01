using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.EntityClasses;

namespace tajmautAPI.Interfaces_Service
{
    public interface IVenueService
    {
        Task<ServiceResponse<VenueRESPONSE>> CreateVenue(VenuePostREQUEST request);
        Task<ServiceResponse<VenueRESPONSE>> DeleteVenue(int venueId);
        Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenues();
        Task<ServiceResponse<List<VenueRESPONSE>>> FilterVenuesByCity(string city);
        Task<ServiceResponse<List<VenueRESPONSE>>> GetVenuesWithOtherData(List<Venue> rest);
        Task<ServiceResponse<VenueRESPONSE>> UpdateVenue(int venueId, VenuePutREQUEST request);
        Task<ServiceResponse<VenueRESPONSE>> GetVenueById(int venueId);
        Task<ServiceResponse<List<Venue_Types>>> GetAllVenueTypes();
        Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenuesByVenueTypeID(int id);
    }
}