using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public ReservationRepository(tajmautDataContext ctx,IHelperValidationClassService helper)
        {
            _ctx= ctx;
            _helper= helper;
        }

        public async Task<OnlineReservation> CreateReservation(ReservationREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var reservation = new OnlineReservation
            {
                RestaurantId = request.RestaurantId,
                UserId = currentUserID,
                EventId = request.EventId,
                NumberGuests = request.NumberGuests,
                Phone = request.Phone,
                FullName = request.FullName,
                ModifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
            };

            _ctx.OnlineReservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<bool> DeleteReservation(OnlineReservation onlineReservation)
        {
            _ctx.OnlineReservations.Remove(onlineReservation);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<List<OnlineReservation>> GetAllGetReservationsByUser(int userId)
        {
            var check = await _ctx.OnlineReservations.Where(us=>us.UserId== userId).ToListAsync();
            if(check.Count()>0)
            {
                return check;
            }
            return null;
        }

        public async Task<List<OnlineReservation>> GetAllReservations()
        {
            return await _ctx.OnlineReservations.ToListAsync();
        }

        public async Task<OnlineReservation> GetReservationByID(int reservationId)
        {
            var check = await _ctx.OnlineReservations.FindAsync(reservationId);
            if (check != null)
            {
                return check;
            }
            return null;
        }

        public async Task<bool> ReservationExistsID(int reservationId)
        {
            var check = await _ctx.OnlineReservations.FindAsync(reservationId);
            if(check!= null)
            {
                return true;
            }
            return false;
        }
    }
}
