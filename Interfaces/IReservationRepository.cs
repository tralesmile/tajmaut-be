using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces
{
    public interface IReservationRepository
    {
        Task<OnlineReservation> CreateReservation(ReservationREQUEST request);
        Task<bool> ReservationExistsID(int reservationId);
        Task<OnlineReservation> GetReservationByID(int reservationId);
        Task<bool> DeleteReservation(OnlineReservation onlineReservation);
        Task<List<OnlineReservation>> GetAllGetReservationsByUser(int userId);
        Task<List<OnlineReservation>> GetAllReservations();
    }
}
