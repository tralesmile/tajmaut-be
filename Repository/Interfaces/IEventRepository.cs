using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Interface for Event repository which handles data access methods related to events.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="request">Request object containing details of the event to be created.</param>
        /// <returns>The newly created event.</returns>
        Task<Event> CreateEvent(EventPostREQUEST request);
        /// <summary>
        /// Adds an event to the database.
        /// </summary>
        /// <param name="eventDB">The event to be added to the database.</param>
        /// <returns>The added event.</returns>
        Task<Event> AddToDB(Event eventDB);

        /// <summary>
        /// Retrieves all events.
        /// </summary>
        /// <returns>A list of all events.</returns>
        Task<List<Event>> GetAllEvents();

        /// <summary>
        /// Retrieves events by ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve.</param>
        /// <returns>A list of events with the specified ID.</returns>
        Task<List<Event>> GetEventById(int eventId);

        /// <summary>
        /// Deletes an event.
        /// </summary>
        /// <param name="eventId">The ID of the event to delete.</param>
        /// <returns>The deleted event.</returns>
        Task<Event> DeleteEvent(int eventId);

        /// <summary>
        /// Deletes an event from the database.
        /// </summary>
        /// <param name="getEvent">The event to be deleted from the database.</param>
        /// <returns>The deleted event.</returns>
        Task<Event> DeleteEventDB(Event getEvent);

        /// <summary>
        /// Updates an event.
        /// </summary>
        /// <param name="request">Request object containing the updated details of the event.</param>
        /// <param name="eventId">The ID of the event to be updated.</param>
        /// <returns>The updated event.</returns>
        Task<Event> UpdateEvent(EventPostREQUEST request, int eventId);

        /// <summary>
        /// Saves updates made to an event to the database.
        /// </summary>
        /// <param name="getEvent">The event to be updated in the database.</param>
        /// <param name="request">Request object containing the updated details of the event.</param>
        /// <returns>The updated event.</returns>
        Task<Event> SaveUpdatesEventDB(Event getEvent, EventPostREQUEST request);

        /// <summary>
        /// Filters events based on the city.
        /// </summary>
        /// <param name="city">The city to filter events by.</param>
        /// <returns>A list of events filtered by the specified city.</returns>
        Task<List<Event>> FilterEventsInCity(string city);

        /// <summary>
        /// Retrieves a venue by ID.
        /// </summary>
        /// <param name="id">The ID of the venue to retrieve.</param>
        /// <returns>The venue with the specified ID.</returns>
        Task<Venue> GetVenueById(int id);

        /// <summary>
        /// Cancels an event.
        /// </summary>
        /// <param name="eventId">The ID of the event to cancel.</param>
        /// <returns>The cancelled event.</returns>
        Task<Event> UpdateCancelEvent(int eventId);


    }
}
