using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;

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

        //get all reservations - admin access
        [HttpGet("GetAllReservations"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        //get reservations by restaurant - admin,manager access
        [HttpGet("GetReservationsByRestaurant"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> GetReservationsByRestaurant(int restaurantId)
        {
            var result = await _reservationService.GetReservationsByRestaurant(restaurantId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        //get reservations by event - admin,manager access 
        [HttpGet("GetReservationsByEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> GetReservationsByEvent(int eventId)
        {
            var result = await _reservationService.GetReservationsByEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        //get reservations by user - user,admin,manager access
        [HttpGet("GetReservationsByUser"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetReservationsByUser(int userId)
        {
            var result = await _reservationService.GetReservationsByUser(userId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        //create reservation user,admin,manager access
        [HttpPost("CreateReservation"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> CreateReservation(ReservationREQUEST request)
        {
            var result = await _reservationService.CreateReservation(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        //delete reservation user,admin,manager access
        [HttpDelete("DeleteReservation"), Authorize(Roles = "Admin,User,Manager")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservation(reservationId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Deleted");
        }

        //change reservation status
        [HttpPut("ManagerStatusReservation"), Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult> ManagerStatusReservation(int reservationId)
        {
            var result = await _reservationService.ManagerStatusReservation(reservationId);

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
