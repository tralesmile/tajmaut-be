using System.Runtime.CompilerServices;
using tajmautAPI.Data;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public EventRepository(tajmautDataContext ctx,IHelperValidationClassService helper)
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
            //check if category and restaurant exist
            if(await _helper.CheckIdRestaurant(request.RestaurantId) && await _helper.CheckIdCategory(request.CategoryEventId))
            {
                return new Event
                {
                    RestaurantId= request.RestaurantId,
                    CategoryEventId= request.CategoryEventId,
                    Name= request.Name,
                    Description= request.Description,
                    EventImage= request.EventImage,
                    DateTime= request.DateTime,
                };

            }
            return null;
        }

        //delete event
        public async Task<Event> DeleteEvent(int eventId)
        {
            //check if exists
            return await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
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
                .Include(e=>e.Restaurant)
                .Where(e=>e.Restaurant.City == city)
                .ToListAsync();

            return eventsInCity;
        }

        //get all events
        public async Task<List<Event>> GetAllEvents()
        {
            return await _ctx.Events.ToListAsync();
        }

        //get event by id
        public async Task<List<Event>> GetEventById(int eventId)
        {
            return await _ctx.Events.Where(n => n.EventId == eventId).ToListAsync();
        }

        //get restaurant by id
        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _ctx.Restaurants.FirstOrDefaultAsync(n => n.RestaurantId == id);
        }

        //save updates in DB
        public async Task<Event> SaveUpdatesEventDB(Event getEvent,EventPostREQUEST request)
        {
            getEvent.RestaurantId= request.RestaurantId;
            getEvent.CategoryEventId= request.CategoryEventId;
            getEvent.Description= request.Description;
            getEvent.EventImage= request.EventImage;
            getEvent.DateTime= request.DateTime;
            getEvent.Name= request.Name;

            await _ctx.SaveChangesAsync();

            return getEvent;
        }

        //update status of event cancel-active
        public async Task<bool> UpdateCancelEvent(int eventId)
        {
            //check if exists
            var result = await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);

            if(result == null)
            {
                return false;
            }

            //if is canceled make active
            if(result.isCanceled)
            {
                result.isCanceled = false;
            }
            //make canceled
            else
            {
                result.isCanceled = true;
            }

            await _ctx.SaveChangesAsync();

            return true;
        }

        //update event by id
        public async Task<Event> UpdateEvent(EventPostREQUEST request, int eventId)
        {
            return await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
        }
    }
}
