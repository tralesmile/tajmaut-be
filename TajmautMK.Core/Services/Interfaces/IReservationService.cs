using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;

namespace TajmautMK.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for managing reservations.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="request">The reservation details.</param>
        /// <returns>A service response containing the created reservation.</returns>
        Task<ServiceResponse<ReservationRESPONSE>> CreateReservation(ReservationREQUEST request);

        /// <summary>
        /// Deletes a reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to delete.</param>
        /// <returns>A service response indicating the success or failure of the operation.</returns>
        Task<ServiceResponse<ReservationRESPONSE>> DeleteReservation(int reservationId);

        /// <summary>
        /// Gets all reservations for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A service response containing the list of reservations.</returns>
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByUser(int userId);

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns>A service response containing the list of reservations.</returns>
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetAllReservations();

        /// <summary>
        /// Gets all reservations for an event.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <returns>A service response containing the list of reservations.</returns>
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId);

        /// <summary>
        /// Gets all reservations for a venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue.</param>
        /// <returns>A service response containing the list of reservations.</returns>
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByVenue(int venueId);

        /// <summary>
        /// Changes the status of a reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to change the status of.</param>
        /// <returns>A service response containing the updated reservation.</returns>
        Task<ServiceResponse<ReservationRESPONSE>> ManagerStatusReservation(int reservationId);
    }
}
