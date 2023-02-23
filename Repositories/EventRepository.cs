using System.Runtime.CompilerServices;
using tajmautAPI.Data;
using tajmautAPI.Interfaces;
using tajmautAPI.Models;

namespace tajmautAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly tajmautDataContext _ctx;
        public EventRepository(tajmautDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Event> AddToDB(Event eventDB)
        {
            _ctx.Events.Add(eventDB);
            await _ctx.SaveChangesAsync();
            return eventDB;
        }

        public async Task<bool> CheckIdCategory(int id)
        {
            var check = await _ctx.CategoryEvents.FirstOrDefaultAsync(cat => cat.CategoryEventId == id);
            if (check != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckIdRestaurant(int id)
        {
            var check = await _ctx.Restaurants.FirstOrDefaultAsync(res => res.RestaurantId== id);
            if(check != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Event> CreateEvent(EventPOST request)
        {
            if(await CheckIdRestaurant(request.RestaurantId) && await CheckIdCategory(request.CategoryEventId))
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

        public async Task<Event> DeleteEvent(int eventId)
        {
            return await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
        }

        public async Task<Event> DeleteEventDB(Event getEvent)
        {
            _ctx.Events.Remove(getEvent);
            await _ctx.SaveChangesAsync();
            return getEvent;
        }

        public async Task<List<Event>> FilterEventsInCity(string city)
        {
            var eventsInCity = await _ctx.Events
                .Include(e=>e.Restaurant)
                .Where(e=>e.Restaurant.City == city)
                .ToListAsync();
            return eventsInCity;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _ctx.Events.ToListAsync();
        }

        public async Task<List<Event>> GetEventById(int eventId)
        {
            return await _ctx.Events.Where(n => n.EventId == eventId).ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _ctx.Restaurants.FirstOrDefaultAsync(n => n.RestaurantId == id);
        }

        public async Task<Event> SaveUpdatesEventDB(Event getEvent,EventPOST request)
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

        public async Task<Event> UpdateEvent(EventPOST request, int eventId)
        {
            return await _ctx.Events.FirstOrDefaultAsync(n => n.EventId == eventId);
        }
    }
}
