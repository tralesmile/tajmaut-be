using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;

namespace tajmautAPI.Services.Interfaces
{
    /// <summary>
    /// Service for validation helper methods
    /// </summary>
    public interface IHelperValidationClassService
    {
        /// <summary>
        /// Validates an email address using a regular expression.
        /// </summary>
        /// <param name="emailRegex">The email address to validate.</param>
        /// <returns>True if the email is valid, false otherwise.</returns>
        bool ValidateEmailRegex(string emailRegex);

        /// <summary>
        /// Checks if there is already a user with the given email.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>A User object if a user with the given email exists, null otherwise.</returns>
        Task<User> CheckDuplicatesEmail(string email);

        /// <summary>
        /// Checks for duplicates when updating a user's email.
        /// </summary>
        /// <param name="email">The new email address to check.</param>
        /// <param name="id">The ID of the user being updated.</param>
        /// <returns>A User object if a user with the given email exists, null otherwise.</returns>
        Task<User> CheckDuplicatesEmailWithId(string email, int id);

        /// <summary>
        /// Checks if a venue with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the venue to check.</param>
        /// <returns>True if a venue with the given ID exists, false otherwise.</returns>
        Task<bool> CheckIdVenue(int id);

        /// <summary>
        /// Checks if a category with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the category to check.</param>
        /// <returns>True if a category with the given ID exists, false otherwise.</returns>
        Task<bool> CheckIdCategory(int id);

        /// <summary>
        /// Checks if a comment with the given ID exists.
        /// </summary>
        /// <param name="commentId">The ID of the comment to check.</param>
        /// <returns>True if a comment with the given ID exists, false otherwise.</returns>
        Task<bool> CheckIdComment(int commentId);

        /// <summary>
        /// Gets the ID of the current user.
        /// </summary>
        /// <returns>The ID of the current user.</returns>
        int GetMe();

        /// <summary>
        /// Gets the email address of the current user.
        /// </summary>
        /// <returns>The email address of the current user.</returns>
        string GetCurrentUserEmail();

        /// <summary>
        /// Gets the role of the current user.
        /// </summary>
        /// <returns>The role of the current user.</returns>
        string GetCurrentUserRole();

        /// <summary>
        /// Checks if an event with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the event to check.</param>
        /// <returns>True if an event with the given ID exists, false otherwise.</returns>
        Task<bool> CheckIdEvent(int id);

        /// <summary>
        /// Checks if a relationship between the given venue and event exists.
        /// </summary>
        /// <param name="venueId">The ID of the venue to check.</param>
        /// <param name="eventId">The ID of the event to check.</param>
        /// <returns>True if a relationship between the given venue and event exists, false otherwise.</returns>
        Task<bool> CheckEventVenueRelation(int venueId, int eventId);

        /// <summary>
        /// Checks if the given event activity ID is valid.
        /// </summary>
        /// <param name="id">The event activity ID to check.</param>
        /// <returns>True if the ID is valid, false otherwise.</returns>
        Task<bool> CheckIdEventActivity(int id);

        /// <summary>
        /// Checks if the given event date ID is valid.
        /// </summary>
        /// <param name="id">The event date ID to check.</param>
        /// <returns>True if the ID is valid, false otherwise.</returns>
        Task<bool> CheckIdEventDate(int id);

        /// <summary>
        /// Checks if the given venue type ID is valid.
        /// </summary>
        /// <param name="id">The venue type ID to check.</param>
        /// <returns>True if the ID is valid, false otherwise.</returns>
        Task<bool> CheckVenueTypeId(int id);

        /// <summary>
        /// Validates the given phone number using regex.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if the phone number is valid, false otherwise.</returns>
        bool ValidatePhoneRegex(string phone);

        /// <summary>
        /// Checks if the given user ID is valid.
        /// </summary>
        /// <param name="id">The user ID to check.</param>
        /// <returns>True if the ID is valid, false otherwise.</returns>
        Task<bool> CheckIdUser(int id);

        /// <summary>
        /// Checks if the current user is an admin.
        /// </summary>
        /// <returns>True if the current user is an admin, false otherwise.</returns>
        bool CheckUserAdmin();

        /// <summary>
        /// Checks if the current user is an admin or manager.
        /// </summary>
        /// <returns>True if the current user is an admin or manager, false otherwise.</returns>
        bool CheckUserAdminOrManager();

        /// <summary>
        /// Checks if the current user is a manager.
        /// </summary>
        /// <returns>True if the current user is a manager, false otherwise.</returns>
        bool CheckUserManager();

        /// <summary>
        /// Validates the given ID.
        /// </summary>
        /// <param name="id">The ID to validate.</param>
        /// <returns>True if the ID is valid, false otherwise.</returns>
        bool ValidateId(int id);

        /// <summary>
        /// Checks if the given reservation ID is valid.
        /// </summary>
        /// <param name="id">The reservation ID to check.</param>
        /// <returns>The online reservation with the given ID if it exists, null otherwise.</returns>
        Task<OnlineReservation> CheckIdReservation(int id);

        /// <summary>
        /// Gets the comment with the given ID.
        /// </summary>
        /// <param name="id">The comment ID to get.</param>
        /// <returns>The comment with the given ID if it exists, null otherwise.</returns>
        Task<Comment> GetCommentId(int id);

        /// <summary>
        /// Gets the user with the given email.
        /// </summary>
        /// <param name="email">The email of the user to get.</param>
        /// <returns>The user with the given email if it exists, null otherwise.</returns>
        Task<User> GetUserWithEmail(string email);

        /// <summary>
        /// Checks if manager is related to specific venue
        /// </summary>
        /// <param name="venueId">The ID of the venue type to check.</param>
        /// <param name="managerId">The ID of the venue type to check.</param>
        /// <returns>True if a venue is related to manager, otherwise false.</returns>
        Task<bool> CheckManagerVenueRelation(int venueId, int managerId);

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

        /// <summary>
        /// Items pagination.
        /// </summary>
        /// <param name="request">Pagination object.</param>
        /// <param name="items">List of items to use pagination on.</param>
        /// <returns>A list of items using pagination.</returns>
        FilterRESPONSE<T> Paginator<T>(BaseFilterREQUEST request,List<T> items);
    }
}

