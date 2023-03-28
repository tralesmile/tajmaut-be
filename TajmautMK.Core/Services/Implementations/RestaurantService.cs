using AutoMapper;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Models;
using tajmautAPI.Services.Interfaces;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;

namespace tajmautAPI.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;

        public RestaurantService(IRestaurantRepository repo, IMapper mapper, IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
        }
        // create restaurant and checks if restaurant exist
        public async Task<ServiceResponse<RestaurantRESPONSE>> CreateRestaurantAsync(RestaurantPostREQUEST request)
        {
            ServiceResponse<RestaurantRESPONSE> result = new();
            try
            {
                    var getResult = await _repo.CreateRestaurantAsync(request);

                    if (getResult != null)
                    {
                        var resultSend = await _repo.AddRestaurantToDB(getResult);
                        result.Data = _mapper.Map<RestaurantRESPONSE>(resultSend);
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

        // Deletes a specific restaurant form the DB by it's id
        public async Task<ServiceResponse<RestaurantRESPONSE>> DeleteRestaurantAsync(int restaurantId)
        { 
         ServiceResponse<RestaurantRESPONSE> result = new();

            try
            {
                if (_helper.ValidateId(restaurantId))
                {
                    var currentUserID = _helper.GetMe();
                    var currentUserRole = _helper.CheckUserAdminOrManager(); 

                    if ((restaurantId == currentUserID) || _helper.CheckUserAdminOrManager())
                    {
                        var restaurant = await _repo.DeleteRestaurantAsync(restaurantId);

                        if (restaurant != null)
                        {
                            var resultSend = await _repo.DeleteRestaurantDB(restaurant); 
                            result.Data = _mapper.Map<RestaurantRESPONSE>(resultSend);
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

        // gets all resraurants
        public async Task<ServiceResponse<List<RestaurantRESPONSE>>> GetAllRestaurantsAsync()
        {

            ServiceResponse<List<RestaurantRESPONSE>> result = new();

            try
            {
                var resultSend = await _repo.GetAllRestaurantsAsync();
                if (resultSend != null)
                {
                    result.Data = _mapper.Map<List<RestaurantRESPONSE>>(resultSend);
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
        //filter restaurants by city
        public async Task<ServiceResponse<List<RestaurantRESPONSE>>> FilterRestaurantsByCity(string city)
        {
            ServiceResponse<List<RestaurantRESPONSE>> result = new();

            try
            {
                // invalid input
                if (city == null)
                    throw new CustomError(400, "Invalid input!");

                // filter
                var restaurants = await _repo.FilterRestaurantsByCity(city);
                if (restaurants.Count() > 0)
                {
                    result = await GetRestaurantsWithOtherData(restaurants);
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

        //get other data for the restaurant in sorted list
        public async Task<ServiceResponse<List<RestaurantRESPONSE>>> GetRestaurantsWithOtherData(List<Restaurant> rest)
        {
            ServiceResponse<List<RestaurantRESPONSE>> result = new ServiceResponse<List<RestaurantRESPONSE>>();

            try
            {
                if (rest != null && rest.Any())
                {
                    var restaurantDetails = await _repo.GetAllRestaurantsAsync();

                    var getRestaurant = rest.Select(rest =>
                    {
                        var details = restaurantDetails.FirstOrDefault(details => details.RestaurantId == rest.RestaurantId);

                        if (details != null)
                        {
                            return new RestaurantRESPONSE
                            {
                                RestaurantId = details.RestaurantId,
                                Name = details.Name,
                                Email = details.Email,
                                Phone = details.Phone,
                                Address = details.Address,
                                City = details.City,
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }).Where(r => r != null).ToList();

                    result.Data = getRestaurant;
                }
                else
                {
                    throw new CustomError(400, "Invalid request data");
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
        //gets a specific restaurant by it's's id
        public async Task<ServiceResponse<RestaurantRESPONSE>> GetRestaurantByIdAsync(int restaurantId)
        {
            ServiceResponse<RestaurantRESPONSE> result = new();

            try
            {
                if (_helper.ValidateId(restaurantId))
                {
                    var resultSend = await _repo.GetRestaurantsIdAsync(restaurantId);
                    result.Data = _mapper.Map<RestaurantRESPONSE>(resultSend);
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
        // checks if restaurant exists and updates it to the DB
        public async Task<ServiceResponse<RestaurantRESPONSE>> UpdateRestaurantAsync(int restaurantId, RestaurantPutREQUEST request)
        {
            var response = new ServiceResponse<RestaurantRESPONSE>();

            try
            {
                if (_helper.ValidateId(restaurantId))
                {
                    var updatedRestaurant = await _repo.UpdateRestaurantAsync(request, restaurantId);

                    if (updatedRestaurant != null)
                    {
                        if (await _helper.CheckIdRestaurant((int)updatedRestaurant.RestaurantId))
                        {
                            var savedRestaurant = await _repo.SaveUpdatesRestaurantDB(updatedRestaurant, request);
                            response.Data = _mapper.Map<RestaurantRESPONSE>(savedRestaurant);
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                response.isError = true;
                response.statusCode = ex.StatusCode;
                response.errorMessage = ex.ErrorMessage;
            }

            return response;
        }
    }
}