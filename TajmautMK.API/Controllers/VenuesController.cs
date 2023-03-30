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
        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet("GetAllVenues"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenues()
        {
            var result = await _venueService.GetAllVenues();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        [HttpGet("FilterVenuesByCity"), AllowAnonymous]
        public async Task<ActionResult> FilterVenuesByCity(string city)
        {

            var result = await _venueService.FilterVenuesByCity(city);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        [HttpGet("GetVenueByID"), AllowAnonymous]
        public async Task<ActionResult> GetVenueByID(int VenueId)
        {

            var result = await _venueService.GetVenueById(VenueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }


        [HttpPost("CreateVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CreateVenue(VenuePostREQUEST request)
        {
            var result = await _venueService.CreateVenue(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }


        [HttpPut("UpdateVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> UpdateVenue(VenuePutREQUEST request, int VenueId)
        {
            // Get result from service
            var result = await _venueService.UpdateVenue(VenueId, request);

            // Check if an error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);

            }  
            // If no error, return success with updated data
                return Ok(result.Data);
        }

        [HttpDelete("DeleteVenue"), Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteVenue(int VenueId)
        {

            var result = await _venueService.DeleteVenue(VenueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Venue Deleted");

        }

        [HttpGet("GetAllVenueTypes"), AllowAnonymous]
        public async Task<ActionResult> GetAllVenueTypes()
        {

            var result = await _venueService.GetAllVenueTypes();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }
    }
}