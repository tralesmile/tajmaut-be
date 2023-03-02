using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

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
        [HttpGet("GetAllEvents"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();

            if(result == null || result.Count()==0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //get event by id
        [HttpGet("GetEventByID"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetEventById(int eventId)
        {
            var result = await _eventService.GetEventById(eventId);

            if(result== null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //all events in a specific restaurant
        [HttpGet("GetRestaurantEventsByRestaurantID"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetAllEventsByRestaurant(int restaurantId)
        {
            var result = await _eventService.GetAllEventsByRestaurant(restaurantId);

            if(result== null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //create event
        [HttpPost("CreateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateEvent(EventPostREQUEST request)
        {
            var checkResult = await _eventService.CreateEvent(request);

            if(checkResult != null)
            {
                return Ok(checkResult);
            }
            return BadRequest("Errors Occured!");
        }

        //update event
        [HttpPut("UpdateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateEvent(EventPostREQUEST request,int eventId)
        {
            var result = await _eventService.UpdateEvent(request, eventId);

            if(result!=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        //delete event
        [HttpDelete("DeleteEventByID"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {
            var result = await _eventService.DeleteEvent(eventId);

            if(result == null)
            {
                return BadRequest();
            }
            return Ok("Event Deleted!");
        }

        //filter events by category
        [HttpGet("FilterEventsByCategory"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByCategory(int categoryId)
        {
            var result = await _eventService.FilterEventsByCategory(categoryId);

            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //filter events by date
        [HttpGet("FilterEventsByDate"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByDate(DateTime startDate,DateTime endDate)
        {
            var result = await _eventService.FilterEventsByDate(startDate, endDate);

            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //filter events by city
        [HttpGet("FilterEventsByCity"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByCity(string city)
        {
            var result = await _eventService.FilterEventsByCity(city);

            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        //filter events by restaurant rating
        [HttpGet("FilterEventsByRestaurantRating"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByRating()
        {
            return Ok();
        }

        //change event status ( cancel event or activate event )
        [HttpPut("EventStatusChange"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CancelEvent(int eventId)
        {
            var result = await _eventService.CancelEvent(eventId);

            if (!result)
            {
                return BadRequest("Bad Request!");
            }
            return Ok("Event status changed!");
        }
    }
}
