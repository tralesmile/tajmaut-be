using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
namespace tajmautAPI.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllRestaurantsAsync();
        Task<List<Restaurant>> FilterRestaurantsByCity(string city);
        Task<Restaurant> GetRestaurantsIdAsync(int RestaurantId);
        Task<Restaurant> CreateRestaurantAsync(RestaurantPostREQUEST request);
        Task<Restaurant> UpdateRestaurantAsync(RestaurantPutREQUEST request, int RestaurantId);
        Task<Restaurant> SaveChanges(Restaurant restaurant, RestaurantPutREQUEST request);
        Task<Restaurant> DeleteRestaurantAsync(int RestaurantId);
        Task<Restaurant> DeleteRestaurantDB(Restaurant result);
        Task<Restaurant> AddRestaurantToDB(Restaurant result);
        Task<Restaurant> SaveUpdatesRestaurantDB(Restaurant resultRestaurant, RestaurantPutREQUEST request);
        Task<Restaurant> CheckRestaurantId(int restaurantId);
    }
}