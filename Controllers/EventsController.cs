using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("GetAllEvents")]
        public async Task<ActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();
            if(result == null || result.Count()==0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetEventByID")]
        public async Task<ActionResult> GetEventById(int eventId)
        {
            var result = await _eventService.GetEventById(eventId);
            if(result== null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("GetRestaurantEventsByRestaurantID")]
        public async Task<ActionResult> GetAllEventsByRestaurant(int restaurantId)
        {
            var result = await _eventService.GetAllEventsByRestaurant(restaurantId);
            if(result== null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("CreateEvent")]
        public async Task<ActionResult> CreateEvent(EventPOST request)
        {
            var checkResult = await _eventService.CreateEvent(request);
            if(checkResult != null)
            {
                return Ok(checkResult);
            }
            return BadRequest("Errors Occured!");
        }

        [HttpPut("UpdateEvent")]
        public async Task<ActionResult> UpdateEvent(EventPOST request,int eventId)
        {
            var result = await _eventService.UpdateEvent(request, eventId);
            if(result!=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteEventByID")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {
            var result = await _eventService.DeleteEvent(eventId);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok("Event Deleted!");
        }

        [HttpGet("FilterEventsByCategory")]
        public async Task<ActionResult> FilterEventsByCategory(int categoryId)
        {
            var result = await _eventService.FilterEventsByCategory(categoryId);
            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("FilterEventsByDate")]
        public async Task<ActionResult> FilterEventsByDate(DateTime startDate,DateTime endDate)
        {
            var result = await _eventService.FilterEventsByDate(startDate, endDate);
            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("FilterEventsByCity")]
        public async Task<ActionResult> FilterEventsByCity(string city)
        {
            var result = await _eventService.FilterEventsByCity(city);
            if(result==null || result.Count()==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("FilterEventsByRestaurantRating")]
        public async Task<ActionResult> FilterEventsByRating()
        {
            return Ok();
        }
    }
}
