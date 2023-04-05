using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Interface for accessing venue data.
    /// </summary>
    public interface IVenueRepository
    {
        /// <summary>
        /// Gets all venues.
        /// </summary>
        /// <returns>A list of venues.</returns>
        Task<List<Venue>> GetAllVenuesAsync();

        /// <summary>
        /// Filters venues by city.
        /// </summary>
        /// <param name="city">The city to filter by.</param>
        /// <returns>A list of venues in the specified city.</returns>
        Task<List<Venue>> FilterVenuesByCity(string city);

        /// <summary>
        /// Gets a venue by ID.
        /// </summary>
        /// <param name="venueId">The ID of the venue to get.</param>
        /// <returns>The venue with the specified ID, or null if no venue is found.</returns>
        Task<Venue> GetVenuesIdAsync(int venueId);

        /// <summary>
        /// Creates a new venue.
        /// </summary>
        /// <param name="request">The request containing information about the venue to create.</param>
        /// <returns>The newly created venue.</returns>
        Task<Venue> CreateVenueAsync(VenuePostREQUEST request);

        /// <summary>
        /// Updates an existing venue.
        /// </summary>
        /// <param name="request">The request containing updated information about the venue.</param>
        /// <param name="venueId">The ID of the venue to update.</param>
        /// <returns>The updated venue.</returns>
        Task<Venue> UpdateVenueAsync(VenuePutREQUEST request, int venueId);

        /// <summary>
        /// Saves changes to a venue.
        /// </summary>
        /// <param name="venue">The venue to save changes to.</param>
        /// <param name="request">The request containing updated information about the venue.</param>
        /// <returns>The saved venue.</returns>
        Task<Venue> SaveChanges(Venue venue, VenuePutREQUEST request);

        /// <summary>
        /// Deletes a venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to delete.</param>
        /// <returns>The deleted venue.</returns>
        Task<Venue> DeleteVenueAsync(int venueId);

        /// <summary>
        /// Deletes a venue from the database.
        /// </summary>
        /// <param name="result">The venue to delete.</param>
        /// <returns>The deleted venue.</returns>
        Task<Venue> DeleteVenueDB(Venue result);

        /// <summary>
        /// Adds a venue to the database.
        /// </summary>
        /// <param name="result">The venue to add.</param>
        /// <returns>The added venue.</returns>
        Task<Venue> AddVenueToDB(Venue result);

        /// <summary>
        /// Saves updates to a venue in the database.
        /// </summary>
        /// <param name="result">The venue to update.</param>
        /// <param name="request">The request containing updated information about the venue.</param>
        /// <returns>The updated venue.</returns>
        Task<Venue> SaveUpdatesVenueDB(Venue result, VenuePutREQUEST request);

        /// <summary>
        /// Checks if a venue with the specified ID exists.
        /// </summary>
        /// <param name="venueId">The ID of the venue to check.</param>
        /// <returns>True if the venue exists, false otherwise.</returns>
        Task<Venue> CheckVenueId(int venueId);

        /// <summary>
        /// Checks if a venue type with the specified ID exists.
        /// </summary>
        /// <param name="venueTypeId">The ID of the venue type to check.</param>
        /// <returns>True if the venue type exists, false otherwise.</returns>
        Task<bool> CheckVenueTypeId(int venueTypeId);

        /// <summary>
        /// Gets all venue types.
        /// </summary>
        /// <returns>A list of all venue types.</returns>
        Task<List<Venue_Types>> GetAllVenueTypes();

        /// <summary>
        /// Gets all venues with the specified venue type ID.
        /// </summary>
        /// <param name="id">The ID of the venue type to filter by.</param>
        /// <returns>A list of venues with the specified venue type ID.</returns>
        Task<List<Venue>> GetAllVenuesByVenueTypeID(int id);
    }
}