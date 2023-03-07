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
            return Ok();
        }

        [HttpGet("GetReservationsByRestaurant"),Authorize(Roles ="Admin,Manager")]
        public async Task<ActionResult> GetReservationsByRestaurant(int restaurantId)
        {
            return Ok();
        }

        [HttpGet("GetReservationsByEvent"),Authorize(Roles ="Admin,Manager")]
        public async Task<ActionResult> GetReservationsByEvent(int eventId)
        {
            return Ok();
        }

        [HttpGet("GetReservationsByUser"),Authorize(Roles ="Admin,Manager,User")]
        public async Task<ActionResult> GetReservationsByUser(int userId)
        {
            return Ok();
        }

        [HttpPut("ChangeReservationStatus"),Authorize(Roles = "Admin,User,Manager")]
        public async Task<ActionResult> ChangeReservationStatus(int reservationId)
        {
            return Ok();
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

        [HttpPut("UpdateReservation"),Authorize(Roles ="User,Admin,Manager")]
        public async Task<ActionResult> UpdateReservation(ReservationREQUEST request,int reservationId)
        {
            return Ok();
        }

        [HttpDelete("DeleteReservation"),Authorize(Roles ="Admin,User,Manager")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            return Ok();
        }
    }
}
