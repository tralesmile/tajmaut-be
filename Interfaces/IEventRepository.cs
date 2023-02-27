using tajmautAPI.Models;

namespace tajmautAPI.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> CreateEvent(EventPOST request);
        Task<bool> CheckIdRestaurant(int id);
        Task<bool> CheckIdCategory(int id);
        Task<Event> AddToDB(Event eventDB);
        Task<List<Event>> GetAllEvents();
        Task<List<Event>> GetEventById(int eventId);
        Task<Event> DeleteEvent(int eventId);
        Task<Event> DeleteEventDB(Event getEvent);
        Task<Event> UpdateEvent(EventPOST request, int eventId);
        Task<Event> SaveUpdatesEventDB(Event getEvent,EventPOST request);
        Task<List<Event>> FilterEventsInCity(string city);
        Task<Restaurant> GetRestaurantById(int id);
        Task<bool> UpdateCancelEvent(int eventId);


    }
}
