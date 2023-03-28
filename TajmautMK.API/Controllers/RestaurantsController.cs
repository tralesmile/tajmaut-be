using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("Get All Restaurants"), AllowAnonymous]
        public async Task<ActionResult> GetAllRestaurantsAsync()
        {
            var result = await _restaurantService.GetAllRestaurantsAsync();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        [HttpGet("Filter Restaurants By City"), AllowAnonymous]
        public async Task<ActionResult> FilterRestaurantsByCity(string city)
        {

            var result = await _restaurantService.FilterRestaurantsByCity(city);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        [HttpGet("Get Restaurant By ID"), AllowAnonymous]
        public async Task<ActionResult> GetRestaurantByIdAsync(int RestaurantId)
        {

            var result = await _restaurantService.GetRestaurantByIdAsync(RestaurantId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }


        [HttpPost("Create Restaurant"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateRestaurantAsync(RestaurantPostREQUEST request)
        {
            var result = await _restaurantService.CreateRestaurantAsync(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }


        [HttpPut("Update Restaurant"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateRestaurantAsync(RestaurantPutREQUEST request, int restaurantId)
        {
            // Get result from service
            var result = await _restaurantService.UpdateRestaurantAsync(restaurantId, request);

            // Check if an error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);

            }  
            // If no error, return success with updated data
                return Ok(result.Data);
        }

        [HttpDelete("Delete Restaurant"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteRestaurantAsync(int RestaurantId)
        {

            var result = await _restaurantService.DeleteRestaurantAsync(RestaurantId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Restaurant Deleted");

        }
    }
}