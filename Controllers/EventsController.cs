using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tajmautAPI.Exceptions;
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
            try
            {
                var result = await _eventService.GetAllEvents();
                return Ok(result);
            }
            catch(Exception ex)
            {
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
            }

            return StatusCode(500);

        }

        //get event by id
        [HttpGet("GetEventByID"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetEventById(int eventId)
        {

            try
            {
                var result = await _eventService.GetEventById(eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //all events in a specific restaurant
        [HttpGet("GetRestaurantEventsByRestaurantID"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetAllEventsByRestaurant(int restaurantId)
        {

            try
            {
                var result = await _eventService.GetAllEventsByRestaurant(restaurantId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //create event
        [HttpPost("CreateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateEvent(EventPostREQUEST request)
        {

            try
            {
                var result = await _eventService.CreateEvent(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //update event
        [HttpPut("UpdateEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateEvent(EventPostREQUEST request,int eventId)
        {

            try
            {
                var result = await _eventService.UpdateEvent(request,eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //delete event
        [HttpDelete("DeleteEventByID"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {

            try
            {
                var result = await _eventService.DeleteEvent(eventId);
                return Ok("Event Deleted");
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //filter events by category
        [HttpGet("FilterEventsByCategory"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByCategory(int categoryId)
        {

            try
            {
                var result = await _eventService.FilterEventsByCategory(categoryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //filter events by date
        [HttpGet("FilterEventsByDate"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByDate(DateTime startDate,DateTime endDate)
        {

            try
            {
                var result = await _eventService.FilterEventsByDate(startDate,endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }

        //filter events by city
        [HttpGet("FilterEventsByCity"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> FilterEventsByCity(string city)
        {

            try
            {
                var result = await _eventService.FilterEventsByCity(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

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

            try
            {
                var result = await _eventService.CancelEvent(eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);

        }
    }
}
