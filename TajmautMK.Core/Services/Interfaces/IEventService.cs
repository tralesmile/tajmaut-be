using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for event-related services.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="request">The request object containing information about the event to create.</param>
        /// <returns>A service response containing the created event.</returns>
        Task<ServiceResponse<EventRESPONSE>> CreateEvent(EventPostREQUEST request);

        /// <summary>
        /// Gets all events.
        /// </summary>
        /// <returns>A service response containing a list of all events.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEvents();

        /// <summary>
        /// Gets an event by its ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve.</param>
        /// <returns>A service response containing the retrieved event.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetEventById(int eventId);

        /// <summary>
        /// Gets all events for a given venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to retrieve events for.</param>
        /// <returns>A service response containing a list of events for the given venue.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEventsByVenue(int venueId);

        /// <summary>
        /// Deletes an event.
        /// </summary>
        /// <param name="eventId">The ID of the event to delete.</param>
        /// <returns>A service response containing the deleted event.</returns>
        Task<ServiceResponse<EventRESPONSE>> DeleteEvent(int eventId);

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="request">The request object containing updated information about the event.</param>
        /// <param name="eventId">The ID of the event to update.</param>
        /// <returns>A service response containing the updated event.</returns>
        Task<ServiceResponse<EventRESPONSE>> UpdateEvent(EventPostREQUEST request, int eventId);

        /// <summary>
        /// Filters events by category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to filter by.</param>
        /// <returns>A service response containing a list of events that match the specified category.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCategory(int categoryId);

        /// <summary>
        /// Filters events by city.
        /// </summary>
        /// <param name="city">The name of the city to filter by.</param>
        /// <returns>A service response containing a list of events that take place in the specified city.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCity(string city);

        /// <summary>
        /// Filters events by date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A service response containing a list of events that fall within the specified date range.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets additional data for a list of events.
        /// </summary>
        /// <param name="events">The list of events to get data for.</param>
        /// <returns>A list of events with additional data.</returns>
        Task<List<EventGetRESPONSE>> GetEventsWithOtherData(List<Event> events);

        /// <summary>
        /// Cancels an event.
        /// </summary>
        /// <param name="id">The ID of the event to cancel.</param>
        /// <returns>A service response indicating whether the event was successfully canceled.</returns>
        Task<ServiceResponse<EventRESPONSE>> CancelEvent(int id);

        /// <summary>
        /// Gets a specified number of events.
        /// </summary>
        /// <param name="numEvents">The number of events to retrieve.</param>
        /// <returns>A service response containing a list of the specified number of events.</returns>
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetNumberOfEvents(int numEvents);
    }
}
