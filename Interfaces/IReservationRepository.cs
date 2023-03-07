using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces
{
    public interface IReservationRepository
    {
        Task<OnlineReservation> CreateReservation(ReservationREQUEST request);
    }
}
