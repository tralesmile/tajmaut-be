using System.Net;
using tajmautAPI.Exceptions;
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
            var check = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if(check== null)
            {
                return check;
            }

            throw new CustomException(HttpStatusCode.BadRequest, $"User exists");
        }

        //check duplicates without the current user
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            var check = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserId != id);
            if(check != null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, $"User exists");
            }
            return check;
        }

        //check if category exists in DB
        public async Task<bool> CheckIdCategory(int id)
        {
            var check = await _ctx.CategoryEvents.FirstOrDefaultAsync(cat => cat.CategoryEventId == id);

            if (check != null)
            {
                return true;
            }
            throw new CustomException(HttpStatusCode.NotFound,"Category not found");

        }

        //check if comment exists
        public async Task<bool> CheckIdComment(int id)
        {
            var check = await _ctx.Comments.FirstOrDefaultAsync(x=>x.CommentId== id);
            if(check!= null)
            {
                return true;
            }
            throw new CustomException(HttpStatusCode.NotFound, $"Comment not found");
        }

        //check if event exists
        public async Task<bool> CheckIdEvent(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(res=>res.EventId == id);
            if(check!=null)
            {
                return true;
            }
            throw new CustomException(HttpStatusCode.NotFound, $"Event not found");
        }

        //check if events is canceled
        public async Task<bool> CheckIdEventActivity(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(eve=>eve.EventId == id);
            if(check.isCanceled)
            {
                throw new CustomException(HttpStatusCode.BadRequest, $"Event was canceled");
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
            throw new CustomException(HttpStatusCode.BadRequest, $"Event ended!");
        }

        //check id reservation
        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            var result = await _ctx.OnlineReservations.FindAsync(id);
            if(result!=null)
            {
                return result;
            }
            throw new CustomException(HttpStatusCode.NotFound, $"Reservation not found");
        }

        //check if restaurant exists in DB
        public async Task<bool> CheckIdRestaurant(int id)
        {
            var check = await _ctx.Restaurants.FirstOrDefaultAsync(res => res.RestaurantId == id);

            if (check != null)
            {
                return true;
            }

            throw new CustomException(HttpStatusCode.NotFound, $"Restaurant not found");
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

        //get comment with id
        public async Task<Comment> GetCommentId(int id)
        {
            var check = await _ctx.Comments.FindAsync(id);
            if(check != null)
            {
                return check;
            }
            throw new CustomException(HttpStatusCode.NotFound, $"Comment not found");
        }
    }
}
