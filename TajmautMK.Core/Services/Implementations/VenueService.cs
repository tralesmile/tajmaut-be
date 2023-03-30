using AutoMapper;
using TajmautMK.Repository.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Interfaces;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using Azure.Core;
using TajmautMK.Common.Models.EntityClasses;

namespace tajmautAPI.Services.Implementations
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;

        public VenueService(IVenueRepository repo, IMapper mapper, IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
        }
        // create restaurant and checks if restaurant exist
        public async Task<ServiceResponse<VenueRESPONSE>> CreateVenue(VenuePostREQUEST request)
        {
            ServiceResponse<VenueRESPONSE> result = new();
            try
            {
                if (await _repo.CheckVenueTypeId(request.VenueTypeId))
                {
                    var getResult = await _repo.CreateVenueAsync(request);

                    if (getResult != null)
                    {
                        var resultSend = await _repo.AddVenueToDB(getResult);
                        result.Data = _mapper.Map<VenueRESPONSE>(resultSend);
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

        // Deletes a specific restaurant form the DB by it's id
        public async Task<ServiceResponse<VenueRESPONSE>> DeleteVenue(int venueId)
        { 
         ServiceResponse<VenueRESPONSE> result = new();

            try
            {
                if (_helper.ValidateId(venueId))
                {
                    var currentUserID = _helper.GetMe();
                    var currentUserRole = _helper.CheckUserAdminOrManager(); 

                    if ((venueId == currentUserID) || _helper.CheckUserAdminOrManager())
                    {
                        var venue = await _repo.DeleteVenueAsync(venueId);

                        if (venue != null)
                        {
                            var resultSend = await _repo.DeleteVenueDB(venue); 
                            result.Data = _mapper.Map<VenueRESPONSE>(resultSend);
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
        public async Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenues()
        {

            ServiceResponse<List<VenueRESPONSE>> result = new();

            try
            {
                var resultSend = await _repo.GetAllVenuesAsync();
                if (resultSend != null)
                {
                    result.Data = _mapper.Map<List<VenueRESPONSE>>(resultSend);
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
        public async Task<ServiceResponse<List<VenueRESPONSE>>> FilterVenuesByCity(string city)
        {
            ServiceResponse<List<VenueRESPONSE>> result = new();

            try
            {
                // invalid input
                if (city == null)
                    throw new CustomError(400, "Invalid input!");

                // filter
                var venues = await _repo.FilterVenuesByCity(city);
                if (venues.Count() > 0)
                {
                    result = await GetVenuesWithOtherData(venues);
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
        public async Task<ServiceResponse<List<VenueRESPONSE>>> GetVenuesWithOtherData(List<Venue> venue)
        {
            ServiceResponse<List<VenueRESPONSE>> result = new ServiceResponse<List<VenueRESPONSE>>();

            try
            {
                if (venue != null && venue.Any())
                {
                    var venueDetails = await _repo.GetAllVenuesAsync();

                    var getVenue = venue.Select(venue =>
                    {
                        var details = venueDetails.FirstOrDefault(details => details.VenueId == venue.VenueId);

                        if (details != null)
                        {
                            return new VenueRESPONSE
                            {
                                VenueId = details.VenueId,
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

                    result.Data = getVenue;
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
        public async Task<ServiceResponse<VenueRESPONSE>> GetVenueById(int venueId)
        {
            ServiceResponse<VenueRESPONSE> result = new();

            try
            {
                if (_helper.ValidateId(venueId))
                {
                    var resultSend = await _repo.GetVenuesIdAsync(venueId);
                    result.Data = _mapper.Map<VenueRESPONSE>(resultSend);
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
        public async Task<ServiceResponse<VenueRESPONSE>> UpdateVenue(int venueId, VenuePutREQUEST request)
        {
            var response = new ServiceResponse<VenueRESPONSE>();

            try
            {
                if (_helper.ValidateId(venueId))
                {
                    var updateVenue = await _repo.UpdateVenueAsync(request, venueId);

                    if (updateVenue != null)
                    {
                        if (await _helper.CheckIdVenue((int)updateVenue.VenueId))
                        {
                            var savedVenue = await _repo.SaveUpdatesVenueDB(updateVenue, request);
                            response.Data = _mapper.Map<VenueRESPONSE>(savedVenue);
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

        public async Task<ServiceResponse<List<Venue_Types>>> GetAllVenueTypes()
        {
            ServiceResponse<List<Venue_Types>> result = new();

            try
            {
                result.Data = await _repo.GetAllVenueTypes();
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }
    }
}