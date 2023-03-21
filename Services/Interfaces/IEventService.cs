using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    public interface IEventService
    {
        Task<ServiceResponse<EventRESPONSE>> CreateEvent(EventPostREQUEST request);
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEvents();
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetEventById(int eventId);
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetAllEventsByRestaurant(int restaurantId);
        Task<ServiceResponse<EventRESPONSE>> DeleteEvent(int eventId);
        Task<ServiceResponse<EventRESPONSE>> UpdateEvent(EventPostREQUEST request, int eventId);
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCategory(int categoryId);
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByCity(string city);
        Task<ServiceResponse<List<EventGetRESPONSE>>> FilterEventsByDate(DateTime startDate, DateTime endDate);
        Task<List<EventGetRESPONSE>> GetEventsWithOtherData(List<Event> events);
        Task<ServiceResponse<EventRESPONSE>> CancelEvent(int id);
        Task<ServiceResponse<List<EventGetRESPONSE>>> GetNumberOfEvents(int numEvents);
    }
}
