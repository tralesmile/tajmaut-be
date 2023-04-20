using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Interface for managing online reservations
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Creates a new online reservation
        /// </summary>
        /// <param name="request">The reservation request data</param>
        /// <returns>The created OnlineReservation object</returns>
        Task<OnlineReservation> CreateReservation(ReservationREQUEST request);

        /// <summary>
        /// Checks if a reservation with the specified ID exists
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to check</param>
        /// <returns>True if a reservation with the specified ID exists, false otherwise</returns>
        Task<bool> ReservationExistsID(int reservationId);

        /// <summary>
        /// Gets the OnlineReservation object with the specified ID
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to retrieve</param>
        /// <returns>The OnlineReservation object with the specified ID</returns>
        Task<OnlineReservation> GetReservationByID(int reservationId);

        /// <summary>
        /// Deletes the specified online reservation
        /// </summary>
        /// <param name="onlineReservation">The reservation to delete</param>
        /// <returns>True if the reservation was deleted successfully, false otherwise</returns>
        Task<bool> DeleteReservation(OnlineReservation onlineReservation);

        /// <summary>
        /// Gets all reservations for a specific user
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve reservations for</param>
        /// <returns>A list of all reservations for the specified user</returns>
        Task<List<OnlineReservation>> GetAllGetReservationsByUser(int userId);

        /// <summary>
        /// Gets a list of all reservations
        /// </summary>
        /// <returns>A list of all reservations</returns>
        Task<List<OnlineReservation>> GetAllReservations();

        /// <summary>
        /// Changes the status of the specified reservation
        /// </summary>
        /// <param name="onlineReservation">The reservation to change the status for</param>
        /// <returns>True if the status was changed successfully, false otherwise</returns>
        Task<bool> ChangeReservationStatus(OnlineReservation onlineReservation);
    }
}
