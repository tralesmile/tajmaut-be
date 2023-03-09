using tajmautAPI.Interfaces;
using tajmautAPI.Models;

namespace tajmautAPI.Repositories
{
    public class HelperValidationClassRepository : IHelperValidationClassRepository
    {

        private readonly tajmautDataContext _ctx;

        public HelperValidationClassRepository(tajmautDataContext ctx)
        {
            _ctx = ctx;
        }

        //check duplicates
        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        //check duplicates without the current user
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserId != id);
        }

        //check if category exists in DB
        public async Task<bool> CheckIdCategory(int id)
        {
            var check = await _ctx.CategoryEvents.FirstOrDefaultAsync(cat => cat.CategoryEventId == id);

            if (check != null)
            {
                return true;
            }
            return false;
        }

        //check if event exists
        public async Task<bool> CheckIdEvent(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(res=>res.EventId == id);
            if(check!=null)
            {
                return true;
            }
            return false;
        }

        //check if events is canceled
        public async Task<bool> CheckIdEventActivity(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(eve=>eve.EventId == id);
            if(check.isCanceled)
            {
                return false;
            }
            return true;
        }

        //check event date
        public async Task<bool> CheckIdEventDate(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(eve=>eve.EventId==id && eve.DateTime>DateTime.Now);
            if(check!=null)
            {
                return true;
            }
            return false;
        }

        //check id reservation
        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            var result = await _ctx.OnlineReservations.FindAsync(id);
            if(result!=null)
            {
                return result;
            }
            return null;
        }

        //check if restaurant exists in DB
        public async Task<bool> CheckIdRestaurant(int id)
        {
            var check = await _ctx.Restaurants.FirstOrDefaultAsync(res => res.RestaurantId == id);

            if (check != null)
            {
                return true;
            }
            return false;
        }

        //check if user exists
        public async Task<bool> CheckIdUser(int id)
        {
            var check = await _ctx.Users.FindAsync(id);
            if (check != null)
            {
                return true;
            }
            return false;
        }

    }
}
