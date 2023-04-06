﻿using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.EntityClasses;

namespace tajmautAPI.Interfaces_Service
{
    /// <summary>
    /// Represents a service for managing venues.
    /// </summary>
    public interface IVenueService
    {
        /// <summary>
        /// Creates a new venue.
        /// </summary>
        /// <param name="request">The data for the new venue.</param>
        /// <returns>A response containing the newly created venue data.</returns>
        Task<ServiceResponse<VenueRESPONSE>> CreateVenue(VenuePostREQUEST request);

        /// <summary>
        /// Deletes an existing venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to delete.</param>
        /// <returns>A response indicating whether the deletion was successful.</returns>
        Task<ServiceResponse<VenueRESPONSE>> DeleteVenue(int venueId);

        /// <summary>
        /// Gets all existing venues.
        /// </summary>
        /// <returns>A response containing a list of all venues.</returns>
        Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenues();

        /// <summary>
        /// Filters existing venues by city.
        /// </summary>
        /// <param name="city">The name of the city to filter by.</param>
        /// <returns>A response containing a list of venues filtered by the specified city.</returns>
        Task<ServiceResponse<List<VenueRESPONSE>>> FilterVenuesByCity(string city);

        /// <summary>
        /// Updates an existing venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to update.</param>
        /// <param name="request">The updated data for the venue.</param>
        /// <returns>A response containing the updated venue data.</returns>
        Task<ServiceResponse<VenueRESPONSE>> UpdateVenue(int venueId, VenuePutREQUEST request);

        /// <summary>
        /// Gets an existing venue by ID.
        /// </summary>
        /// <param name="venueId">The ID of the venue to get.</param>
        /// <returns>A response containing the venue data.</returns>
        Task<ServiceResponse<VenueRESPONSE>> GetVenueById(int venueId);

        /// <summary>
        /// Gets all existing venue types.
        /// </summary>
        /// <returns>A response containing a list of all venue types.</returns>
        Task<ServiceResponse<List<Venue_Types>>> GetAllVenueTypes();

        /// <summary>
        /// Gets all venues associated with a particular venue type ID.
        /// </summary>
        /// <param name="id">The ID of the venue type to filter by.</param>
        /// <returns>A response containing a list of venues associated with the specified venue type ID.</returns>
        Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenuesByVenueTypeID(int id);

        /// <summary>
        /// Gets all venue cities.
        /// </summary>
        /// <returns>A response containing a list of venues citie.</returns>
        Task<ServiceResponse<List<Venue_City>>> GetAllVenueCities();
    }
}