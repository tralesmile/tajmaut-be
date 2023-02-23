using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

namespace tajmautAPI.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<Event> CreateEvent(EventPOST request)
        {
            var getResult = await _repo.CreateEvent(request);
            if(getResult != null)
            {
                return await _repo.AddToDB(getResult);
            }
            return null;
        }

        public async Task<Event> DeleteEvent(int eventId)
        {
            var result = await _repo.DeleteEvent(eventId);
            if(result != null)
            {
                return await _repo.DeleteEventDB(result);
            }
            return null;
        }

        public async Task<List<EventGET>> FilterEventsByCategory(int categoryId)
        {
            if(await _repo.CheckIdCategory(categoryId))
            {
                var resultEvents = await _repo.GetAllEvents();
                if(resultEvents.Count>0)
                {
                    var listEvents = resultEvents.Where(n=>n.CategoryEventId== categoryId).ToList();
                    return await GetEventsWithOtherData(listEvents);
                }
            }
            return null;
        }

        public async Task<List<EventGET>> FilterEventsByCity(string city)
        {
            var result = await _repo.FilterEventsInCity(city);
            return await GetEventsWithOtherData(result);
        }

        public async Task<List<EventGET>> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            var allEvents = await _repo.GetAllEvents();
            var sendEvents = allEvents.Where(e=>e.DateTime>=startDate && e.DateTime<=endDate).ToList();
            return await GetEventsWithOtherData(sendEvents);
        }

        public async Task<List<EventGET>> GetAllEvents()
        {
            var getResult = await _repo.GetAllEvents();
            return await GetEventsWithOtherData(getResult);
        }

        public async Task<List<EventGET>> GetAllEventsByRestaurant(int restaurantId)
        {
            var allEvents = await _repo.GetAllEvents();
            var listEvents = allEvents.Where(n=>n.RestaurantId== restaurantId).ToList();
            return await GetEventsWithOtherData(listEvents);
        }

        public async Task<List<EventGET>> GetEventById(int eventId)
        {
            var result = await _repo.GetEventById(eventId);
            return await GetEventsWithOtherData(result);

        }

        public async Task<List<EventGET>> GetEventsWithOtherData(List<Event> events)
        {
            if (events != null)
            {
                var now = DateTime.Now;
                var eventsGet = new List<EventGET>();
                foreach (var ev in events)
                {
                    var restaurant = await _repo.GetRestaurantById(ev.RestaurantId);
                    var stausEvent = ev.DateTime > now ? "Upcoming"
                        : (ev.DateTime.AddHours(1) > now ? "Ongoing"
                        : "Ended");
                    eventsGet.Add(new EventGET
                    {
                        EventId = ev.EventId,
                        CategoryEventId = ev.CategoryEventId,
                        RestaurantId = ev.RestaurantId,
                        Name = ev.Name,
                        Description = ev.Description,
                        EventImage = ev.EventImage,
                        DateTime = ev.DateTime,
                        RestaurantName = restaurant.Name,
                        RestaurantPhone = restaurant.Phone,
                        StatusEvent = stausEvent,
                    }); ;

                }
                return eventsGet.ToList();
            }
            return null;
        }

        public async Task<Event> UpdateEvent(EventPOST request, int eventId)
        {
            var resultEvent = await _repo.UpdateEvent(request, eventId);
            if (resultEvent != null)
            {
                if (await _repo.CheckIdCategory(request.CategoryEventId) && await _repo.CheckIdRestaurant(request.RestaurantId))
                    return await _repo.SaveUpdatesEventDB(resultEvent, request);
            }
            return null;
        }
    }
}
