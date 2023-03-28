using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Interfaces_Service
{
    public interface IRestaurantService
    {
        Task<ServiceResponse<RestaurantRESPONSE>> CreateRestaurantAsync(RestaurantPostREQUEST request);
        Task<ServiceResponse<RestaurantRESPONSE>> DeleteRestaurantAsync(int restaurantId);
        Task<ServiceResponse<List<RestaurantRESPONSE>>> GetAllRestaurantsAsync();
        Task<ServiceResponse<List<RestaurantRESPONSE>>> FilterRestaurantsByCity(string city);
        Task<ServiceResponse<List<RestaurantRESPONSE>>> GetRestaurantsWithOtherData(List<Restaurant> rest);
        Task<ServiceResponse<RestaurantRESPONSE>> UpdateRestaurantAsync(int restaurantId, RestaurantPutREQUEST request);
        Task<ServiceResponse<RestaurantRESPONSE>> GetRestaurantByIdAsync(int restaurantId);
    }
}