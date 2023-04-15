using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Net;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
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
            if (check == null)
            {
                return check;
            }

            throw new CustomError(409, $"User exists");
        }

        //check duplicates without the current user
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            var check = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserId != id);
            if (check != null)
            {
                throw new CustomError(409, $"User exists");
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
            throw new CustomError(404, "Category not found");

        }

        //check if comment exists
        public async Task<bool> CheckIdComment(int id)
        {
            var check = await _ctx.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            if (check != null)
            {
                return true;
            }
            throw new CustomError(404, $"Comment not found");
        }

        //check if event exists
        public async Task<bool> CheckIdEvent(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(res => res.EventId == id);
            if (check != null)
            {
                return true;
            }
            throw new CustomError(404, $"Event not found");
        }

        //check if events is canceled
        public async Task<bool> CheckIdEventActivity(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(eve => eve.EventId == id);
            if (check.isCanceled)
            {
                throw new CustomError(400, $"Event was canceled");
            }
            return true;
        }

        //check event date
        public async Task<bool> CheckIdEventDate(int id)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(eve => eve.EventId == id && eve.DateTime > DateTime.Now);
            if (check != null)
            {
                return true;
            }
            throw new CustomError(400, $"Event ended!");
        }

        //check id reservation
        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            var result = await _ctx.OnlineReservations.FindAsync(id);
            if (result != null)
            {
                return result;
            }
            throw new CustomError(404, $"Reservation not found");
        }

        //check if restaurant exists in DB
        public async Task<bool> CheckIdVenue(int id)
        {
            var check = await _ctx.Venues.FirstOrDefaultAsync(res => res.VenueId == id);

            if (check != null)
            {
                return true;
            }

            throw new CustomError(404, $"Venue not found");
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
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, $"Comment not found");
        }

        public async Task<User> GetUserWithEmail(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                return user;
            }
            throw new CustomError(404, $"User not found!");
        }

        //if a specific restaurant has that event
        public async Task<bool> CheckEventVenueRelation(int venueId, int eventId)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(v => v.VenueId == venueId && v.EventId==eventId);
            if(check != null)
            {
                return true;
            }
            throw new CustomError(400, $"Invalid Venue or Event");
        }

        public async Task<bool> CheckVenueTypeId(int id)
        {
            var check = await _ctx.VenueTypes.FirstOrDefaultAsync(v => v.Venue_TypesId == id);
            if(check!=null)
            {
                return true;
            }

            throw new CustomError(404, $"Venue type not found!");
        }

        public async Task<bool> CheckManagerVenueRelation(int venueId, int managerId)
        {
            var check = await _ctx.Venues.FirstOrDefaultAsync(x => x.VenueId == venueId && x.ManagerId == managerId);
            if(check != null)
            {
                return true;
            }

            throw new CustomError(401, $"You dont have rights to manage this venue!");
        }

        public async Task<Event> GetEventByID(int eventId)
        {
            var check = await _ctx.Events.Include(x => x.CategoryEvent).FirstOrDefaultAsync(x=>x.EventId== eventId);
            if (check != null)
            {
                return check;
            }

            throw new CustomError(404, $"Event not found!");
        }

        public async Task<List<Venue>> GetVenuesByCityId(int cityId)
        {
            var check = await _ctx.Venues.Where(x=>x.Venue_CityId == cityId).ToListAsync();

            if(check.Count()>0)
            {
                return check;
            }

            throw new CustomError(404, $"This city has no venues!");
        }
    }
}
