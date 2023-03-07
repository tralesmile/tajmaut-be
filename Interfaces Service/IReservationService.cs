using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Interfaces_Service
{
    public interface IReservationService
    {
        Task<ReservationRESPONSE> CreateReservation(ReservationREQUEST request);
    }
}
