using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.DevTools.V108.Page;
using tajmautAPI.Exceptions;
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
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;

        public EventService(IEventRepository repo,IMapper mapper, IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
        }

        //change status of event (canceled or active)
        public async Task<bool> CancelEvent(int eventId)
        {
            if(await _repo.UpdateCancelEvent(eventId))
            {
                return true;
            }
            throw new CustomNotFoundException($"Event with ID:{eventId} not found!");
        }

        //create event
        public async Task<EventRESPONSE> CreateEvent(EventPostREQUEST request)
        {
            //check if category and restaurant exist
            if (await _helper.CheckIdRestaurant(request.RestaurantId))
            {
                if(await _helper.CheckIdCategory(request.CategoryEventId))
                {
                    var getResult = await _repo.CreateEvent(request);
                    if (getResult != null)
                    {
                        var result = await _repo.AddToDB(getResult);
                        return _mapper.Map<EventRESPONSE>(result);
                    }
                }
                else
                {
                    throw new CustomBadRequestException("Invalid category ID");
                }
            }
            else
            {
                throw new CustomBadRequestException("Invalid restaurant ID");
            }

            throw new CustomBadRequestException("Invalid input");
        }

        //delete event by id
        public async Task<EventRESPONSE> DeleteEvent(int eventId)
        {
            //if invalid input
            if (eventId < 0)
                throw new CustomBadRequestException("Invalid ID");

            var result = await _repo.DeleteEvent(eventId);

            if(result != null)
            {
                var returnResult = await _repo.DeleteEventDB(result);
                return _mapper.Map<EventRESPONSE>(returnResult);
            }

            throw new CustomNotFoundException("Event not found!");
            
        }

        //filter events by category
        public async Task<List<EventGetRESPONSE>> FilterEventsByCategory(int categoryId)
        {
            //invalid input
            if (categoryId < 0)
                throw new CustomBadRequestException("Invalid ID");

            //check if category exists
            if(await _helper.CheckIdCategory(categoryId))
            {
                var resultEvents = await _repo.GetAllEvents();

                if(resultEvents.Count()>0)
                {
                    var listEvents = resultEvents.Where(n=>n.CategoryEventId== categoryId).ToList();
                    return await GetEventsWithOtherData(listEvents);
                }
                else
                {
                    throw new CustomNotFoundException("No data found!");
                }
            }
            throw new CustomBadRequestException("Category not found!");
        }

        //filter events by city
        public async Task<List<EventGetRESPONSE>> FilterEventsByCity(string city)
        {
            //invalid input
            if (city == null)
                throw new CustomBadRequestException("Invalid input!");

            //filter
            var result = await _repo.FilterEventsInCity(city);
            if (result.Count()>0)
            {
                return await GetEventsWithOtherData(result);
            }
            throw new CustomNotFoundException("No data found!");
        }

        //filter events by date from-to
        public async Task<List<EventGetRESPONSE>> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            var allEvents = await _repo.GetAllEvents();

            //get all events
            if(allEvents.Count()>0)
            {
                //query
                var sendEvents = allEvents.Where(e => e.DateTime >= startDate && e.DateTime <= endDate).ToList();
                if(sendEvents.Count()>0)
                {
                    return await GetEventsWithOtherData(sendEvents);
                }
                throw new CustomNotFoundException("No data found!");
            }
            throw new CustomNotFoundException("No data found!");

        }

        //get all events to list
        public async Task<List<EventGetRESPONSE>> GetAllEvents()
        {
            var getResult = await _repo.GetAllEvents();

            if(getResult.Count()>0)
            {
                return await GetEventsWithOtherData(getResult);
            }
            throw new CustomNotFoundException("No data found!");
        }

        //get events from specific restaurant
        public async Task<List<EventGetRESPONSE>> GetAllEventsByRestaurant(int restaurantId)
        {
            //invalid input
            if (restaurantId < 0)
                throw new CustomBadRequestException("Invalid ID");

            var allEvents = await _repo.GetAllEvents();
            if (allEvents.Count() > 0)
            {
                //query
                var listEvents = allEvents.Where(n => n.RestaurantId == restaurantId).ToList();
                if(listEvents.Count()>0)
                {
                    return await GetEventsWithOtherData(listEvents);
                }
                throw new CustomNotFoundException("No data found!");
            }
            throw new CustomNotFoundException("No data found!");

        }

        //get event by id
        public async Task<List<EventGetRESPONSE>> GetEventById(int eventId)
        {

            //invalid input
            if (eventId < 0)
                throw new CustomBadRequestException("Invalid ID");

            var result = await _repo.GetEventById(eventId);

            if(result.Count() > 0)
            {
                return await GetEventsWithOtherData(result);
            }

            throw new CustomNotFoundException("No data found!");
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
                    });

                }

                //sort event
                eventsGet.Sort((x,y)=>x.DateTime.CompareTo(y.DateTime));

                return eventsGet.ToList();
            }
            return null;
        }

        //update event
        public async Task<EventRESPONSE> UpdateEvent(EventPostREQUEST request, int eventId)
        {

            //invalid input
            if (eventId < 0)
                throw new CustomBadRequestException("Invalid ID");

            var resultEvent = await _repo.UpdateEvent(request, eventId);

            //if found
            if (resultEvent != null)
            {
                //check if restaurant and category exist
                if (await _helper.CheckIdCategory(request.CategoryEventId))
                {
                    if(await _helper.CheckIdRestaurant(request.RestaurantId))
                    {
                        var result = await _repo.SaveUpdatesEventDB(resultEvent, request);
                        return _mapper.Map<EventRESPONSE>(result);
                    }
                    else
                    {
                        throw new CustomBadRequestException("Invalid Restaurant ID");
                    }
                }
                else
                {
                    throw new CustomBadRequestException("Invalid Category ID");
                }
            }
            throw new CustomNotFoundException("No data found");
        }
    }
}
