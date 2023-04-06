using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VenuesController"/> class.
        /// </summary>
        /// <param name="venueService">The venue service.</param>
        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        /// <summary>
        /// Gets all venues.
        /// </summary>
        /// <returns>The list of venues.</returns>
        [HttpGet("GetAllVenues"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenues()
        {
            var result = await _venueService.GetAllVenues();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Filters venues by city.
        /// </summary>
        /// <param name="city">The name of the city to filter by.</param>
        /// <returns>The list of venues that match the specified city.</returns>
        [HttpGet("FilterVenuesByCity"), AllowAnonymous]
        public async Task<ActionResult> FilterVenuesByCity(string city)
        {

            var result = await _venueService.FilterVenuesByCity(city);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Gets a venue by its ID.
        /// </summary>
        /// <param name="VenueId">The ID of the venue to get.</param>
        /// <returns>The venue with the specified ID.</returns>
        [HttpGet("GetVenueByID"), AllowAnonymous]
        public async Task<ActionResult> GetVenueByID(int VenueId)
        {

            var result = await _venueService.GetVenueById(VenueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Creates a new venue.
        /// </summary>
        /// <param name="request">The request data for the new venue.</param>
        /// <returns>The created venue.</returns>
        [HttpPost("CreateVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateVenue(VenuePostREQUEST request)
        {
            var result = await _venueService.CreateVenue(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Updates an existing venue.
        /// </summary>
        /// <param name="request">The request data for the updated venue.</param>
        /// <param name="VenueId">The ID of the venue to update.</param>
        [HttpPut("UpdateVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateVenue(VenuePutREQUEST request, int VenueId)
        {
            // Get result from service
            var result = await _venueService.UpdateVenue(VenueId, request);

            // Check if an error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);

            }  
            // If no error, return success with updated data
                return Ok(result.Data);
        }

        /// <summary>
        /// Deletes a venue by its ID.
        /// </summary>
        /// <param name="VenueId">The ID of the venue to delete.</param>
        /// <returns>A message indicating that the venue was deleted.</returns>
        [HttpDelete("DeleteVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteVenue(int VenueId)
        {

            var result = await _venueService.DeleteVenue(VenueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Venue Deleted");

        }

        /// <summary>
        /// Gets all venue types.
        /// </summary>
        /// <returns>The list of venue types.</returns>
        [HttpGet("GetAllVenueTypes"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenueTypes()
        {

            var result = await _venueService.GetAllVenueTypes();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Gets all venues that have a specific venue type.
        /// </summary>
        /// <param name="id">The ID of the venue type to filter by.</param>
        /// <returns>The list of venues that have the specified venue type.</returns>
        [HttpGet("GetAllVenuesByVenueTypeID"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenuesByVenueTypeID(int id)
        {
            var result = await _venueService.GetAllVenuesByVenueTypeID(id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all venue cities.
        /// </summary>
        /// <returns>The list of venue cities.</returns>
        [HttpGet("GetAllVenueCities"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenueCities()
        {
            var result = await _venueService.GetAllVenueCities();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }
    }
}
