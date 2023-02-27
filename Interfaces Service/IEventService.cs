using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models;

namespace tajmautAPI.Interfaces_Service
{
    public interface IEventService
    {
        Task<Event> CreateEvent(EventPOST request);
        Task<List<EventGET>> GetAllEvents();
        Task<List<EventGET>> GetEventById(int eventId);
        Task<List<EventGET>> GetAllEventsByRestaurant(int restaurantId);
        Task<Event> DeleteEvent(int eventId);
        Task<Event> UpdateEvent(EventPOST request, int eventId);
        Task<List<EventGET>> FilterEventsByCategory(int categoryId);
        Task<List<EventGET>> FilterEventsByCity(string city);
        Task<List<EventGET>> FilterEventsByDate(DateTime startDate, DateTime endDate);
        Task<List<EventGET>> GetEventsWithOtherData(List<Event> events);
        Task<bool> CancelEvent(int id);
    }
}
