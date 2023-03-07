using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Interfaces_Service
{
    public interface IReservationService
    {
        Task<ReservationRESPONSE> CreateReservation(ReservationREQUEST request);
        Task<string> DeleteReservation(int reservationId);
        Task<List<ReservationRESPONSE>> GetReservationsByUser(int userId);
        Task<List<ReservationRESPONSE>> GetAllReservations();
        Task<List<ReservationRESPONSE>> GetReservationsByEvent(int eventId);
    }
}
