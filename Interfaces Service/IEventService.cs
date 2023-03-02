using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Interfaces_Service
{
    public interface IEventService
    {
        Task<Event> CreateEvent(EventPostREQUEST request);
        Task<List<EventGetRESPONSE>> GetAllEvents();
        Task<List<EventGetRESPONSE>> GetEventById(int eventId);
        Task<List<EventGetRESPONSE>> GetAllEventsByRestaurant(int restaurantId);
        Task<Event> DeleteEvent(int eventId);
        Task<Event> UpdateEvent(EventPostREQUEST request, int eventId);
        Task<List<EventGetRESPONSE>> FilterEventsByCategory(int categoryId);
        Task<List<EventGetRESPONSE>> FilterEventsByCity(string city);
        Task<List<EventGetRESPONSE>> FilterEventsByDate(DateTime startDate, DateTime endDate);
        Task<List<EventGetRESPONSE>> GetEventsWithOtherData(List<Event> events);
        Task<bool> CancelEvent(int id);
    }
}
