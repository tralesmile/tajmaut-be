using AutoMapper;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Core.Services.Implementations
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
            var currentUserID = _helper.GetMe();
            try
            {
                var eventByID = await _helper.GetEventByID(eventId);
                var venueID = eventByID.VenueId;

                if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                {
                    var getEvent = await _repo.UpdateCancelEvent(eventId);

                    if (getEvent != null)
                    {
                        result.Data = _mapper.Map<EventRESPONSE>(getEvent);
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //create event
        public async Task<ServiceResponse<EventRESPONSE>> CreateEvent(EventPostREQUEST request)
        {

            ServiceResponse<EventRESPONSE> result = new();
            var currentUserID = _helper.GetMe();

            try
            {
                //check if category and venue exist
                if (await _helper.CheckIdVenue(request.VenueId))
                {
                    if (await _helper.CheckManagerVenueRelation(request.VenueId, currentUserID))
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

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //delete event by id
        public async Task<ServiceResponse<EventRESPONSE>> DeleteEvent(int eventId)
        {

            ServiceResponse<EventRESPONSE> result = new();
            var currentUserID = _helper.GetMe();
            var eventByID = await _helper.GetEventByID(eventId);
            var venueID = eventByID.VenueId;

            try
            {
                //if invalid input
                if (_helper.ValidateId(eventId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                    {
                        var resultSend = await _repo.DeleteEvent(eventId);

                        if (resultSend != null)
                        {
                            var returnResult = await _repo.DeleteEventDB(resultSend);
                            result.Data = _mapper.Map<EventRESPONSE>(returnResult);
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //filter events by category
        public async Task<ServiceResponse<FilterRESPONSE<EventGetRESPONSE>>> FilterEvents(EventFilterREQUEST request)
        {

            ServiceResponse<FilterRESPONSE<EventGetRESPONSE>> result = new();

            try
            {

                var requestSend = new BaseFilterREQUEST
                {
                    ItemsPerPage = request.ItemsPerPage,
                    PageNumber = request.PageNumber,
                };

                var response = _helper.Paginator(request, _mapper.Map<List<EventGetRESPONSE>>(SortEvents(await _repo.EventFilter(request))));

                result.Data = response;
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
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
                    var sortEvents = SortEvents(resultSend);
                    result.Data = _mapper.Map<List<EventGetRESPONSE>>(sortEvents);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
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
                    var sendEvents = allEvents.Where(e => e.DateTime >= startDate && e.DateTime <= endDate.AddDays(1)).ToList();
                    if (sendEvents.Count() > 0)
                    {
                        var sortEvents = SortEvents(sendEvents);
                        result.Data = _mapper.Map<List<EventGetRESPONSE>>(sortEvents);
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
                result.ErrorMessage = ex.ErrorMessage;
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
                    var sortedEvents = SortEvents(getResult);
                    result.Data = _mapper.Map<List<EventGetRESPONSE>>(sortedEvents);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //get events from specific restaurant
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEventsByVenue(int venueId)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //invalid input
                if (_helper.ValidateId(venueId))
                {
                    if (await _helper.CheckIdVenue(venueId))
                    {
                        var allEvents = await _repo.GetAllEvents();

                        if (allEvents.Count() > 0)
                        {
                            //query
                            var listEvents = allEvents.Where(n => n.VenueId == venueId).ToList();
                            if (listEvents.Count() > 0)
                            {
                                var sortedEvents = SortEvents(listEvents);
                                result.Data = _mapper.Map<List<EventGetRESPONSE>>(sortedEvents);
                            }
                            else
                            {
                                throw new CustomError(404, "This venue has no events!");
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
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
                        result.Data = _mapper.Map<List<EventGetRESPONSE>>(resultSend);
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //get number of events
        public async Task<ServiceResponse<List<EventGetRESPONSE>>> GetNumberOfEvents(int numEvents)
        {

            ServiceResponse<List<EventGetRESPONSE>> result = new();

            try
            {
                //get all events
                var allEvents = await _repo.GetAllEvents();

                var now = DateTime.Now;
                //upcoming events filter
                var upcomingEvents = allEvents.Where(x => x.DateTime > now).OrderBy(x => x.DateTime).ToList();

                    //check if any
                    if (upcomingEvents.Count() > 0)
                    {
                        var sortEvents = _mapper.Map<List<EventGetRESPONSE>>(SortEvents(upcomingEvents));
                        

                        //take num events
                        var getNumEvents = sortEvents.Take(numEvents).ToList();

                        result.Data = getNumEvents;

                    }
                    else
                    {
                        throw new CustomError(404, $"No upcoming events");
                    }
               
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public List<Event> SortEvents(List<Event> items)
        {
            items.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));

            return items.ToList();
        }

        //update event
        public async Task<ServiceResponse<EventRESPONSE>> UpdateEvent(EventPostREQUEST request, int eventId)
        {

            ServiceResponse<EventRESPONSE> result = new();
            var currentUserID = _helper.GetMe();
            var eventByID = await _helper.GetEventByID(eventId);
            var venueID = eventByID.VenueId;

            try
            {
                //invalid input
                if (_helper.ValidateId(eventId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                    {
                        var resultEvent = await _repo.UpdateEvent(request, eventId);

                        //if found
                        if (resultEvent != null)
                        {
                            //check if restaurant and category exist
                            if (await _helper.CheckIdCategory(request.CategoryEventId))
                            {
                                //if restaurant exists
                                if (await _helper.CheckIdVenue(request.VenueId))
                                {
                                    var resultSend = await _repo.SaveUpdatesEventDB(resultEvent, request);
                                    result.Data = _mapper.Map<EventRESPONSE>(resultSend);
                                }
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

    }
}
