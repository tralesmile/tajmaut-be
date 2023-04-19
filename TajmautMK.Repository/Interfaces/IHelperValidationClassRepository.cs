using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Repository for handling various helper validation functions.
    /// </summary>
    public interface IHelperValidationClassRepository
    {
        /// <summary>
        /// Check if there is a user with the same email.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>The User object if there is a user with the same email, otherwise null.</returns>
        Task<User> CheckDuplicatesEmail(string email);

        /// <summary>
        /// Check for duplicates when updating a user.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <param name="id">The id of the user being updated.</param>
        /// <returns>The User object if there is a user with the same email and different id, otherwise null.</returns>
        Task<User> CheckDuplicatesEmailWithId(string email, int id);

        /// <summary>
        /// Check if a venue with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the venue to check</param>
        /// <returns>True if a venue with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdVenue(int id);

        /// <summary>
        /// Check if a category with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the category to check</param>
        /// <returns>True if a category with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdCategory(int id);

        /// <summary>
        /// Check if an event with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the event to check</param>
        /// <returns>True if an event with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdEvent(int id);

        /// <summary>
        /// Check if an event activity with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the event activity to check</param>
        /// <returns>True if an event activity with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdEventActivity(int id);

        /// <summary>
        /// Check if an event date with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the event date to check</param>
        /// <returns>True if an event date with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdEventDate(int id);

        /// <summary>
        /// Check if a user with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the user to check</param>
        /// <returns>True if a user with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdUser(int id);

        /// <summary>
        /// Check if a comment with the specified ID exists in the database
        /// </summary>
        /// <param name="id">The ID of the comment to check</param>
        /// <returns>True if a comment with the specified ID exists, false otherwise</returns>
        Task<bool> CheckIdComment(int id);

        /// <summary>
        /// Check if an event is related to a venue in the database
        /// </summary>
        /// <param name="venueId">The ID of the venue to check</param>
        /// <param name="eventId">The ID of the event to check</param>
        /// <returns>True if the event is related to the venue, false otherwise</returns>
        Task<bool> CheckEventVenueRelation(int venueId, int eventId);

        /// <summary>
        /// Get the comment with the specified id.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        /// <returns>The Comment object if it exists, otherwise null.</returns>
        Task<Comment> GetCommentId(int id);

        /// <summary>
        /// Check if there is a reservation with the specified id.
        /// </summary>
        /// <param name="id">The id of the reservation.</param>
        /// <returns>The OnlineReservation object if it exists, otherwise null.</returns>
        Task<OnlineReservation> CheckIdReservation(int id);

        /// <summary>
        /// Get the user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The User object if it exists, otherwise null.</returns>
        Task<User> GetUserWithEmail(string email);

        /// <summary>
        /// Checks if a venue type with the given ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the venue type to check.</param>
        /// <returns>True if a venue type with the given ID exists in the database, otherwise false.</returns>
        Task<bool> CheckVenueTypeId(int id);

        /// <summary>
        /// Checks if manager is related to specific venue
        /// </summary>
        /// <param name="venueId">The ID of the venue type to check.</param>
        /// <param name="managerId">The ID of the venue type to check.</param>
        /// <returns>True if a venue is related to manager, otherwise false.</returns>
        Task<bool> CheckManagerVenueRelation(int venueId,int managerId);

        /// <summary>
        /// Gets the event by id
        /// </summary>
        /// <param name="eventId">The ID of the venue type to check.</param>
        /// <returns>Event if found ,if not CustomError.</returns>
        Task<Event> GetEventByID(int eventId);

        /// <summary>
        /// Gets the venues by city id
        /// </summary>
        /// <param name="cityId">The ID of the venue city to check.</param>
        /// <returns>List of venues or CustomError.</returns>
        Task<List<Venue>> GetVenuesByCityId(int cityId);

    }
}
