using Microsoft.EntityFrameworkCore;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly tajmautDataContext _context;
        private readonly IHelperValidationClassService _helper;

        public RestaurantRepository(tajmautDataContext context, IHelperValidationClassService helper)
        {
            _context = context;
            _helper = helper;
        }

        // gets all restaurants

        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            var check = await _context.Restaurants.ToListAsync();
            if (check.Count() > 0)
            {
                return check;
            }

            throw new CustomError(404, "No Data found!");

        }
        // filter restaurants by city
        public async Task<List<Restaurant>> FilterRestaurantsByCity(string city)
        {
            var cityRestaurants = await _context.Restaurants
            .Where(e => e.City == city)
            .ToListAsync();

            if (cityRestaurants.Count() > 0)
            {
                return cityRestaurants;
            }

            throw new CustomError(404, $"No data found");
        }
        // get restaurant by it's ID
        public async Task<Restaurant> GetRestaurantsIdAsync(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
            if (restaurant == null)
            {
                throw new CustomError(404, $"Restaurant not found");
            }

            return restaurant;
        }
        // creates restaurant
        public async Task<Restaurant> CreateRestaurantAsync(RestaurantPostREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            return new Restaurant
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                ModifiedBy = currentUserID,
                CreatedBy = currentUserID,
            };
        }
        // updates restaurant
        public async Task<Restaurant> UpdateRestaurantAsync(RestaurantPutREQUEST request, int RestaurantId)
        {
            var restaurant = await _context.Restaurants.FindAsync(RestaurantId);
            if (restaurant != null)
            {
                return restaurant;
            }
            throw new CustomError(404, $"Restaurant not found");
        }
        // saves changes to restraunt
        public async Task<Restaurant> SaveChanges(Restaurant restaurant, RestaurantPutREQUEST request)
        {
            var currentUserID = _helper.GetMe();

            restaurant.Name = request.Name;
            restaurant.Email = request.Email;
            restaurant.Phone = request.Phone;
            restaurant.City = restaurant.City;
            restaurant.Address = restaurant.Address;
            restaurant.ModifiedBy = currentUserID;
            restaurant.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return restaurant;
        }
        // deletes restaurant
        public async Task<Restaurant> DeleteRestaurantAsync(int restaurantId)
        {
            // check's if restaurant exists 
            var check = await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.RestaurantId == restaurantId);
            if (check != null)
            {
                return check;
            }
            throw new CustomError(404, "Restaurant not found!");

        }

        // delete's restaurant from DB
        public async Task<Restaurant> DeleteRestaurantDB(Restaurant getRestaurant)
        {
            _context.Restaurants.Remove(getRestaurant);

            await _context.SaveChangesAsync();

            return getRestaurant;
        }
        // saves & updates restaurant in the DB
        public async Task<Restaurant> SaveUpdatesRestaurantDB(Restaurant getRestaurant, RestaurantPutREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            getRestaurant.Name = request.Name;
            getRestaurant.Email = request.Email;
            getRestaurant.Address = request.Address;
            getRestaurant.City = request.City;
            getRestaurant.Phone = request.Phone;
            getRestaurant.ModifiedBy = currentUserID;
            getRestaurant.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return getRestaurant;
        }
        // adds restaurant to DB
        public async Task<Restaurant> AddRestaurantToDB(Restaurant getResult)
        {
            _context.Restaurants.Add(getResult);
            await _context.SaveChangesAsync();

            return getResult;
        }
        // get restaurant by it's id
        public async Task<Restaurant> CheckRestaurantId(int restaurantId)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.RestaurantId == restaurantId);
        }
    }
}