using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Controllers
{
    /// <summary>
    /// This controller manages all operations related to events.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {

        private readonly IEventService _eventService;

        /// <summary>
        /// Constructor for the EventsController that injects an instance of IEventService.
        /// </summary>
        /// <param name="eventService">An instance of IEventService</param>
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Get all events from the database.
        /// </summary>
        /// <returns>An ActionResult containing the list of events, or an error message if an error occurred.</returns>
        [HttpGet("GetAllEvents"), AllowAnonymous]
        public async Task<ActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Get a specific event by its ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve.</param>
        /// <returns>An ActionResult containing the event, or an error message if an error occurred.</returns>
        [HttpGet("GetEventByID"), AllowAnonymous]
        public async Task<ActionResult> GetEventById(int eventId)
        {

            var result = await _eventService.GetEventById(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Get all events associated with a specific venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to retrieve events for.</param>
        /// <returns>An ActionResult containing the list of events, or an error message if an error occurred.</returns>
        [HttpGet("GetVenueEventsByVenueID"), AllowAnonymous]
        public async Task<ActionResult> GetVenueEventsByVenueID(int venueId)
        {

            var result = await _eventService.GetAllEventsByVenue(venueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Create a new event.
        /// </summary>
        /// <param name="request">A request object containing the details of the new event.</param>
        /// <returns>An ActionResult containing the newly created event, or an error message if an error occurred.</returns>
        [HttpPost("CreateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateEvent(EventPostREQUEST request)
        {

            var result = await _eventService.CreateEvent(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Update an existing event.
        /// </summary>
        /// <param name="request">A request object containing the updated details of the event.</param>
        /// <param name="eventId">The ID of the event to update.</param>
        /// <returns>An ActionResult containing the updated event, or an error message if an error occurred.</returns>
        [HttpPut("UpdateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateEvent(EventPostREQUEST request,int eventId)
        {

            var result = await _eventService.UpdateEvent(request,eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Delete an existing event.
        /// </summary>
        /// <param name="eventId">The ID of the event to delete.</param>
        /// <returns>An ActionResult indicating success or failure of the deletion operation.</returns>
        [HttpDelete("DeleteEventByID"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {

            var result = await _eventService.DeleteEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Event Deleted");

        }

        /// <summary>
        /// Filter events by category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to filter events by.</param>
        /// <returns>An ActionResult containing the list of filtered events, or an error message if an error occurred.</returns>
        [HttpGet("FilterEventsByCategory"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByCategory(int categoryId)
        {

            var result = await _eventService.FilterEventsByCategory(categoryId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Gets events that fall within the specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>The events that fall within the specified date range.</returns>
        [HttpGet("FilterEventsByDate"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByDate(DateTime startDate,DateTime endDate)
        {

            var result = await _eventService.FilterEventsByDate(startDate,endDate);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Gets events in the specified city.
        /// </summary>
        /// <param name="city">The city to filter events by.</param>
        /// <returns>The events in the specified city.</returns>
        [HttpGet("FilterEventsByCity"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByCity(string city)
        {

            var result = await _eventService.FilterEventsByCity(city);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Gets events at venues with a rating above a specified threshold.
        /// </summary>
        /// <returns>The events at venues with a rating above the specified threshold.</returns>
        [HttpGet("FilterEventsByVenueRating"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByVenueRating()
        {
            return Ok();
        }

        /// <summary>
        /// Cancels an event with the specified ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to cancel.</param>
        /// <returns>A message indicating that the event status has changed.</returns>
        [HttpPut("EventStatusChange"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CancelEvent(int eventId)
        {
            var result = await _eventService.CancelEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Event status changed");

        }

        /// <summary>
        /// Gets the specified number of upcoming events.
        /// </summary>
        /// <param name="numEvents">The number of upcoming events to get.</param>
        /// <returns>The specified number of upcoming events.</returns>
        [HttpGet("GetNumberOfEvents"), AllowAnonymous]
        public async Task<ActionResult> GetNumberOfEvents(int numEvents)
        {

            var result = await _eventService.GetNumberOfEvents(numEvents);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }
    }
}
