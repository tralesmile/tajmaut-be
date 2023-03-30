using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    public interface IVenueRepository
    {
        Task<List<Venue>> GetAllVenuesAsync();
        Task<List<Venue>> FilterVenuesByCity(string city);
        Task<Venue> GetVenuesIdAsync(int venueId);
        Task<Venue> CreateVenueAsync(VenuePostREQUEST request);
        Task<Venue> UpdateVenueAsync(VenuePutREQUEST request, int venueId);
        Task<Venue> SaveChanges(Venue venue, VenuePutREQUEST request);
        Task<Venue> DeleteVenueAsync(int venueId);
        Task<Venue> DeleteVenueDB(Venue result);
        Task<Venue> AddVenueToDB(Venue result);
        Task<Venue> SaveUpdatesVenueDB(Venue result, VenuePutREQUEST request);
        Task<Venue> CheckVenueId(int venueId);
        Task<bool> CheckVenueTypeId(int venueTypeId);
        Task<List<Venue_Types>> GetAllVenueTypes();
    }
}