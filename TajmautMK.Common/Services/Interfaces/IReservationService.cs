using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ServiceResponse<ReservationRESPONSE>> CreateReservation(ReservationREQUEST request);
        Task<ServiceResponse<ReservationRESPONSE>> DeleteReservation(int reservationId);
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByUser(int userId);
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetAllReservations();
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId);
        Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByRestaurant(int restaurantId);
        Task<ServiceResponse<ReservationRESPONSE>> ManagerStatusReservation(int reservationId);
    }
}
