using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("GetAllReservations") ,Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();
            try
            {
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpGet("GetReservationsByRestaurant"),Authorize(Roles ="Admin,Manager")]
        public async Task<ActionResult> GetReservationsByRestaurant(int restaurantId)
        {
            return Ok();
        }

        [HttpGet("GetReservationsByEvent"),Authorize(Roles ="Admin,Manager")]
        public async Task<ActionResult> GetReservationsByEvent(int eventId)
        {
            var result = await _reservationService.GetReservationsByEvent(eventId);
            try
            {
                if (result.Count() > 0)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpGet("GetReservationsByUser"),Authorize(Roles ="Admin,Manager,User")]
        public async Task<ActionResult> GetReservationsByUser(int userId)
        {
            var result = await _reservationService.GetReservationsByUser(userId);
            try
            {
                if (result.Count() > 0 || result!=null)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpPost("CreateReservation"),Authorize(Roles ="Admin,Manager,User")]
        public async Task<ActionResult> CreateReservation(ReservationREQUEST request)
        {
            var result = await _reservationService.CreateReservation(request);
            try
            {
                if(result!=null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);

            }

            return StatusCode(500);
        }

        [HttpDelete("DeleteReservation"),Authorize(Roles ="Admin,User,Manager")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservation(reservationId);
            try
            {
                if(result!=null || result!="")
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
            }

            return StatusCode(500);
        }
    }
}
