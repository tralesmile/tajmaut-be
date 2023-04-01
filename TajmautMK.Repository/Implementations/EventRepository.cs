using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public EventRepository(tajmautDataContext ctx, IHelperValidationClassService helper)
        {
            _ctx = ctx;
            _helper = helper;
        }

        //add event to DB
        public async Task<Event> AddToDB(Event eventDB)
        {
            _ctx.Events.Add(eventDB);

            await _ctx.SaveChangesAsync();

            return eventDB;
        }

        //create event
        public async Task<Event> CreateEvent(EventPostREQUEST request)
        {

            //get current user id
            var currentUserID = _helper.GetMe();
            return new Event
            {
                VenueId = request.VenueId,
                CategoryEventId = request.CategoryEventId,
                Name = request.Name,
                Description = request.Description,
                EventImage = request.EventImage,
                DateTime = request.DateTime,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
            };
        }

        //delete event
        public async Task<Event> DeleteEvent(int eventId)
        {
            //check if exists
            var check = await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, "Event not found!");

        }

        //delete event from DB
        public async Task<Event> DeleteEventDB(Event getEvent)
        {
            _ctx.Events.Remove(getEvent);

            await _ctx.SaveChangesAsync();

            return getEvent;
        }

        //filter events by city
        public async Task<List<Event>> FilterEventsInCity(string city)
        {
            //query
            var eventsInCity = await _ctx.Events
                .Include(e => e.Venue)
                .Where(e => e.Venue.City == city)
                .ToListAsync();

            if (eventsInCity.Count() > 0)
            {
                return eventsInCity;
            }

            throw new CustomError(404, $"No data found");
        }

        //get all events
        public async Task<List<Event>> GetAllEvents()
        {
            var check = await _ctx.Events.ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }

            throw new CustomError(404, "No Data found!");

        }

        //get event by id
        public async Task<List<Event>> GetEventById(int eventId)
        {
            var check = await _ctx.Events.Where(n => n.EventId == eventId).ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }

            throw new CustomError(404, $"Event not found");
        }

        //get restaurant by id
        public async Task<Venue> GetVenueById(int id)
        {
            return await _ctx.Venues.FirstOrDefaultAsync(n => n.VenueId == id);
        }

        //save updates in DB
        public async Task<Event> SaveUpdatesEventDB(Event getEvent, EventPostREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            getEvent.VenueId = request.VenueId;
            getEvent.CategoryEventId = request.CategoryEventId;
            getEvent.Description = request.Description;
            getEvent.EventImage = request.EventImage;
            getEvent.DateTime = request.DateTime;
            getEvent.Name = request.Name;
            getEvent.ModifiedBy = currentUserID;
            getEvent.ModifiedAt = DateTime.Now;

            await _ctx.SaveChangesAsync();

            return getEvent;
        }

        //update status of event cancel-active
        public async Task<Event> UpdateCancelEvent(int eventId)
        {
            //check if exists
            var result = await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);

            if (result == null)
            {
                throw new CustomError(404, $"Event with ID:{eventId} not found!");
            }

            //if is canceled make active
            if (result.isCanceled)
            {
                result.isCanceled = false;
            }
            //make canceled
            else
            {
                result.isCanceled = true;
            }

            await _ctx.SaveChangesAsync();

            return result;
        }

        //update event by id
        public async Task<Event> UpdateEvent(EventPostREQUEST request, int eventId)
        {
            var check = await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
            if (check == null)
            {
                throw new CustomError(404, $"Event not found");
            }
            return check;
        }
    }
}
