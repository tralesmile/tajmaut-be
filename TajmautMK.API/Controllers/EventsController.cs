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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {

        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        //get all db events
        [HttpGet("GetAllEvents"), AllowAnonymous]
        public async Task<ActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        //get event by id
        [HttpGet("GetEventByID"), AllowAnonymous]
        public async Task<ActionResult> GetEventById(int eventId)
        {

            var result = await _eventService.GetEventById(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //all events in a specific restaurant
        [HttpGet("GetRestaurantEventsByRestaurantID"), AllowAnonymous]
        public async Task<ActionResult> GetAllEventsByRestaurant(int restaurantId)
        {

            var result = await _eventService.GetAllEventsByRestaurant(restaurantId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        //create event
        [HttpPost("CreateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateEvent(EventPostREQUEST request)
        {

            var result = await _eventService.CreateEvent(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        //update event
        [HttpPut("UpdateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateEvent(EventPostREQUEST request,int eventId)
        {

            var result = await _eventService.UpdateEvent(request,eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //delete event
        [HttpDelete("DeleteEventByID"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {

            var result = await _eventService.DeleteEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Event Deleted");

        }

        //filter events by category
        [HttpGet("FilterEventsByCategory"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByCategory(int categoryId)
        {

            var result = await _eventService.FilterEventsByCategory(categoryId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //filter events by date
        [HttpGet("FilterEventsByDate"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByDate(DateTime startDate,DateTime endDate)
        {

            var result = await _eventService.FilterEventsByDate(startDate,endDate);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //filter events by city
        [HttpGet("FilterEventsByCity"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByCity(string city)
        {

            var result = await _eventService.FilterEventsByCity(city);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //filter events by restaurant rating
        [HttpGet("FilterEventsByRestaurantRating"), AllowAnonymous]
        public async Task<ActionResult> FilterEventsByRating()
        {
            return Ok();
        }

        //change event status ( cancel event or activate event )
        [HttpPut("EventStatusChange"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CancelEvent(int eventId)
        {
            var result = await _eventService.CancelEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Event status changed");

        }

        [HttpGet("GetNumberOfEvents"), AllowAnonymous]
        public async Task<ActionResult> GetNumberOfEvents(int numEvents)
        {

            var result = await _eventService.GetNumberOfEvents(numEvents);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }
    }
}
