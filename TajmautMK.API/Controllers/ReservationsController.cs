using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Core.Services.Interfaces;

namespace TajmautMK.API.Controllers
{
    /// <summary>
    /// API controller for managing reservations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationService _reservationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="reservationService">The reservation service.</param>
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Gets all reservations with admin access.
        /// </summary>
        /// <returns>The list of all reservations.</returns>
        [HttpGet("GetAllReservations"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets reservations by restaurant with admin or manager access.
        /// </summary>
        /// <param name="venueId">The ID of the venue.</param>
        /// <returns>The list of reservations for the venue.</returns>
        [HttpGet("GetReservationsByVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> GetReservationsByVenue(int venueId)
        {
            var result = await _reservationService.GetReservationsByVenue(venueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets reservations by event with admin or manager access.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <returns>The list of reservations for the event.</returns>
        [HttpGet("GetReservationsByEvent"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> GetReservationsByEvent(int eventId)
        {
            var result = await _reservationService.GetReservationsByEvent(eventId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets reservations by user with user, admin, or manager access.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The list of reservations for the user.</returns>
        [HttpGet("GetReservationsByUser"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> GetReservationsByUser(int userId)
        {
            var result = await _reservationService.GetReservationsByUser(userId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a reservation with user, admin, or manager access.
        /// </summary>
        /// <param name="request">The request object containing reservation data.</param>
        /// <returns>The created reservation.</returns>
        [HttpPost("CreateReservation"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> CreateReservation(ReservationREQUEST request)
        {
            var result = await _reservationService.CreateReservation(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Deletes a reservation identified by the given reservation ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to delete.</param>
        /// <returns>Returns an ActionResult indicating whether the reservation was deleted successfully or not.</returns>
        /// <remarks>This action requires the user to have admin, user or manager access.</remarks>
        [HttpDelete("DeleteReservation"), Authorize(Roles = "Admin,User,Manager")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservation(reservationId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Deleted");
        }

        /// <summary>
        /// Changes the status of a reservation identified by the given reservation ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to change status of.</param>
        /// <returns>Returns an ActionResult indicating whether the status was changed successfully or not.</returns>
        [HttpPut("ManagerStatusReservation"), Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult> ManagerStatusReservation(int reservationId)
        {
            var result = await _reservationService.ManagerStatusReservation(reservationId);

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
