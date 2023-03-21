using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.DevTools.V108.Page;
using System.Net;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;

        public EventService(IEventRepository repo, IMapper mapper, IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
        }

        //change status of event (canceled or active)
        public async Task<ServiceResponse<EventRESPONSE>> CancelEvent(int eventId)
        {

            ServiceResponse<EventRESPONSE> result = new();

            try
            {
                var getEvent = await _repo.UpdateCancelEvent(eventId);

                if (getEvent != null)
                {
                    result.Data = _mapper.Map<EventRESPONSE>(getEvent);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //create event
        public async Task<ServiceResponse<EventRESPONSE>> CreateEvent(EventPostREQUEST request)
        {

            ServiceResponse<EventRESPONSE> result = new();

            try
            {
                //check if category and restaurant exist
                if (await _helper.CheckIdRestaurant(request.RestaurantId))
                {
                    //if category exists
                    if (await _helper.CheckIdCategory(request.CategoryEventId))
                    {
                        var getResult = await _repo.CreateEvent(request);

                        if (getResult != null)
                        {
                            var resultSend = await _repo.AddToDB(getResult);
                            result.Data = _mapper.Map<EventRESPONSE>(resultSend);
                        }
                    }
                }

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //delete event by id
        public async Task<ServiceResponse<EventRESPONSE>> DeleteEvent(int eventId)
        {

            ServiceResponse<EventRESPONSE> result = new();

            try
            {
                //if invalid input
                if (_helper.ValidateId(eventId))
                {

                    var resultSend = await _repo.DeleteEvent(eventId);

                    if (resultSend != null)
                    {
                        var returnResult = await _repo.DeleteEventDB(resultSend);
                        result.Data = _mapper.Map<EventRESPONSE>(returnResult);
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //filter events by category
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCategory(int categoryId)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //invalid input
                if (_helper.ValidateId(categoryId))
                {
                    //check if category exists
                    if (await _helper.CheckIdCategory(categoryId))
                    {
                        var resultEvents = await _repo.GetAllEvents();

                        if (resultEvents.Count() > 0)
                        {
                            var listEvents = resultEvents.Where(n => n.CategoryEventId == categoryId).ToList();
                            if (listEvents.Count() > 0)
                            {
                                var resultSend = await GetEventsWithOtherData(listEvents);
                                result.Data = resultSend;
                            }
                            else
                            {
                                throw new CustomError(404, $"This category has no events");
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //filter events by city
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCity(string city)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //invalid input
                if (city == null)
                    throw new CustomError(400, "Invalid input!");

                //filter
                var resultSend = await _repo.FilterEventsInCity(city);
                if (resultSend.Count() > 0)
                {
                    result.Data = await GetEventsWithOtherData(resultSend);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //filter events by date from-to
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                var allEvents = await _repo.GetAllEvents();

                //get all events
                if (allEvents.Count() > 0)
                {
                    //query
                    var sendEvents = allEvents.Where(e => e.DateTime >= startDate && e.DateTime <= endDate).ToList();
                    if (sendEvents.Count() > 0)
                    {
                        result.Data = await GetEventsWithOtherData(sendEvents);
                    }
                    else
                    {
                        throw new CustomError(404, "No data found!");
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //get all events to list
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEvents()
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                var getResult = await _repo.GetAllEvents();

                if (getResult.Count() > 0)
                {
                    result.Data = await GetEventsWithOtherData(getResult);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //get events from specific restaurant
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEventsByRestaurant(int restaurantId)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //invalid input
                if (_helper.ValidateId(restaurantId))
                {
                    if (await _helper.CheckIdRestaurant(restaurantId))
                    {
                        var allEvents = await _repo.GetAllEvents();

                        if (allEvents.Count() > 0)
                        {
                            //query
                            var listEvents = allEvents.Where(n => n.RestaurantId == restaurantId).ToList();
                            if (listEvents.Count() > 0)
                            {
                                result.Data = await GetEventsWithOtherData(listEvents);
                            }
                            else
                            {
                                throw new CustomError(404, "This restaurant has no events!");
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //get event by id
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetEventById(int eventId)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();


            try
            {
                //invalid input
                if (_helper.ValidateId(eventId))
                {

                    var resultSend = await _repo.GetEventById(eventId);

                    if (resultSend.Count() > 0)
                    {
                        result.Data = await GetEventsWithOtherData(resultSend);
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
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
                        : ev.DateTime.AddHours(1) > now ? "Ongoing"
                        : "Ended";
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
                eventsGet.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));

                return eventsGet.ToList();
            }
            return null;
        }

        //get number of events
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetNumberOfEvents(int numEvents)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //get all events
                var allEvents = await _repo.GetAllEvents();

                //check if any
                if (allEvents.Count() > 0)
                {
                    var now = DateTime.Now;
                    //upcoming events filter
                    var upcomingEvents = allEvents.Where(x => x.DateTime > now).OrderBy(x => x.DateTime).ToList();

                    //check if any
                    if (upcomingEvents.Count() > 0)
                    {

                        var getEvents = new List<EventGetRESPONSE>();

                        foreach (var ev in upcomingEvents)
                        {
                            if (!ev.isCanceled)
                            {
                                //get the event restaurant
                                var restaurant = await _repo.GetRestaurantById(ev.RestaurantId);

                                getEvents.Add(new EventGetRESPONSE
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
                                    StatusEvent = "Upcoming",
                                });
                            }
                        }

                        //sord events by date
                        getEvents.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));

                        //take num events
                        var getNumEvents = getEvents.Take(numEvents).ToList();

                        result.Data = getNumEvents;

                    }
                    else
                    {
                        throw new CustomError(404, $"No upcoming events");
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //update event
        public async Task<ServiceResponse<EventRESPONSE>> UpdateEvent(EventPostREQUEST request, int eventId)
        {

            ServiceResponse<EventRESPONSE> result = new();

            try
            {
                //invalid input
                if (_helper.ValidateId(eventId))
                {

                    var resultEvent = await _repo.UpdateEvent(request, eventId);

                    //if found
                    if (resultEvent != null)
                    {
                        //check if restaurant and category exist
                        if (await _helper.CheckIdCategory(request.CategoryEventId))
                        {
                            //if restaurant exists
                            if (await _helper.CheckIdRestaurant(request.RestaurantId))
                            {
                                var resultSend = await _repo.SaveUpdatesEventDB(resultEvent, request);
                                result.Data = _mapper.Map<EventRESPONSE>(resultSend);
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

    }
}
