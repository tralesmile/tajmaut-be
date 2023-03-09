using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Service;

namespace tajmautAPI.Interfaces_Service
{
    public interface IReservationService
    {
        Task<ReservationRESPONSE> CreateReservation(ReservationREQUEST request);
        Task<string> DeleteReservation(int reservationId);
        Task<List<ReservationRESPONSE>> GetReservationsByUser(int userId);
        Task<List<ReservationRESPONSE>> GetAllReservations();
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId);
        Task<List<ReservationRESPONSE>> GetReservationsByRestaurant(int restaurantId);
        Task<ReservationRESPONSE> ManagerStatusReservation(int reservationId);
    }
}
