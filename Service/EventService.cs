using Microsoft.Extensions.Logging;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        //change status of event (canceled or active)
        public async Task<bool> CancelEvent(int eventId)
        {
            if(await _repo.UpdateCancelEvent(eventId))
            {
                return true;
            }
            return false;
        }

        //create event
        public async Task<Event> CreateEvent(EventPostREQUEST request)
        {
            var getResult = await _repo.CreateEvent(request);

            if(getResult != null)
            {
                return await _repo.AddToDB(getResult);
            }
            return null;
        }

        //delete event by id
        public async Task<Event> DeleteEvent(int eventId)
        {
            var result = await _repo.DeleteEvent(eventId);

            if(result != null)
            {
                return await _repo.DeleteEventDB(result);
            }
            return null;
        }

        //filter events by category
        public async Task<List<EventGetRESPONSE>> FilterEventsByCategory(int categoryId)
        {
            //check if category exists
            if(await _repo.CheckIdCategory(categoryId))
            {
                var resultEvents = await _repo.GetAllEvents();

                if(resultEvents.Count()>0)
                {
                    var listEvents = resultEvents.Where(n=>n.CategoryEventId== categoryId).ToList();
                    return await GetEventsWithOtherData(listEvents);
                }
            }
            return null;
        }

        //filter events by city
        public async Task<List<EventGetRESPONSE>> FilterEventsByCity(string city)
        {
            //filter
            var result = await _repo.FilterEventsInCity(city);

            return await GetEventsWithOtherData(result);
        }

        //filter events by date from-to
        public async Task<List<EventGetRESPONSE>> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            var allEvents = await _repo.GetAllEvents();

            //query
            var sendEvents = allEvents.Where(e=>e.DateTime>=startDate && e.DateTime<=endDate).ToList();

            return await GetEventsWithOtherData(sendEvents);
        }

        //get all events to list
        public async Task<List<EventGetRESPONSE>> GetAllEvents()
        {
            var getResult = await _repo.GetAllEvents();
            return await GetEventsWithOtherData(getResult);
        }

        //get events from specific restaurant
        public async Task<List<EventGetRESPONSE>> GetAllEventsByRestaurant(int restaurantId)
        {
            var allEvents = await _repo.GetAllEvents();

            //query
            var listEvents = allEvents.Where(n=>n.RestaurantId== restaurantId).ToList();

            return await GetEventsWithOtherData(listEvents);
        }

        //get event by id
        public async Task<List<EventGetRESPONSE>> GetEventById(int eventId)
        {
            var result = await _repo.GetEventById(eventId);

            return await GetEventsWithOtherData(result);

        }

        //get other data for the events in sorted list
        public async Task<List<EventGetRESPONSE>> GetEventsWithOtherData(List<Event> events)
        {
            if (events != null)
            {
                //date
                var now = DateTime.Now;

                var eventsGet = new List<EventGetRESPONSE>();

                //check status and add events
                foreach (var ev in events)
                {
                    //to get the name and phone of the restaurant
                    var restaurant = await _repo.GetRestaurantById(ev.RestaurantId);

                    //update status of event
                    var statusEvent = "";
                    if (!ev.isCanceled)
                    {
                        statusEvent = ev.DateTime > now ? "Upcoming"
                        : (ev.DateTime.AddHours(1) > now ? "Ongoing"
                        : "Ended");
                    }
                    else
                    {
                        statusEvent = "Canceled";
                    }

                    //add event
                    eventsGet.Add(new EventGetRESPONSE
                    {
                        EventId = ev.EventId,
                        CategoryEventId = ev.CategoryEventId,
                        RestaurantId = ev.RestaurantId,
                        Name = ev.Name,
                        Description = ev.Description,
                        EventImage = ev.EventImage,
                        DateTime = ev.DateTime,
                        isCanceled = ev.isCanceled,
                        RestaurantName = restaurant.Name,
                        RestaurantPhone = restaurant.Phone,
                        StatusEvent = statusEvent,
                    }); ;

                }

                //sort event
                eventsGet.Sort((x,y)=>x.DateTime.CompareTo(y.DateTime));

                return eventsGet.ToList();
            }
            return null;
        }

        //update event
        public async Task<Event> UpdateEvent(EventPostREQUEST request, int eventId)
        {
            var resultEvent = await _repo.UpdateEvent(request, eventId);

            if (resultEvent != null)
            {
                //check if restaurant and category exist
                if (await _repo.CheckIdCategory(request.CategoryEventId) && await _repo.CheckIdRestaurant(request.RestaurantId))
                    return await _repo.SaveUpdatesEventDB(resultEvent, request);
            }
            return null;
        }
    }
}
