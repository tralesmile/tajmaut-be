using AutoMapper;
using TajmautMK.Repository.Interfaces;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Services.Implementations;
using TajmautMK.Common.Interfaces;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;

namespace TajmautMK.Core.Services.Implementations
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
        // create venue and checks if venue exist
        public async Task<ServiceResponse<VenueRESPONSE>> CreateVenue(VenuePostREQUEST request)
        {
            ServiceResponse<VenueRESPONSE> result = new();
            try
            {
                if (await _repo.CheckVenueTypeId(request.VenueTypeId))
                {
                    if (await _repo.CheckVenueCityId(request.Venue_CityId))
                    {

                        var getResult = await _repo.CreateVenueAsync(request);

                        if (getResult != null)
                        {
                            result.Data = _mapper.Map<VenueRESPONSE>(getResult);
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }
            return result;
        }

        // Deletes a specific venue form the DB by it's id
        public async Task<ServiceResponse<VenueRESPONSE>> DeleteVenue(int venueId)
        { 
         ServiceResponse<VenueRESPONSE> result = new();

            try
            {
                var currentUserID = _helper.GetMe();

                if (_helper.ValidateId(venueId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueId, currentUserID))
                    {
                        var currentUserRole = _helper.CheckUserAdminOrManager();

                        if ((venueId == currentUserID) || _helper.CheckUserAdminOrManager())
                        {
                            var venue = await _repo.DeleteVenueAsync(venueId);

                            if (venue != null)
                            {
                                result.Data = _mapper.Map<VenueRESPONSE>(venue);
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        // gets all venue
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
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //filter venue by city
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
                    result.Data = _mapper.Map<List<VenueRESPONSE>>(venues);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //gets a specific venue by it's's id
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
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        // checks if venue exists and updates it to the DB
        public async Task<ServiceResponse<VenueRESPONSE>> UpdateVenue(int venueId, VenuePutREQUEST request)
        {
            var response = new ServiceResponse<VenueRESPONSE>();
            var currentUserID = _helper.GetMe();
            try
            {
                if (_helper.ValidateId(venueId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueId, currentUserID))
                    {
                        var updateVenue = await _repo.GetVenueByID(venueId);

                        if (updateVenue != null)
                        {
                            if (await _helper.CheckIdVenue((int)updateVenue.VenueId))
                            {
                                if (await _repo.CheckVenueCityId(request.Venue_CityId))
                                {
                                    if (await _repo.CheckVenueTypeId(request.VenueTypeId))
                                    {
                                        var savedVenue = await _repo.SaveUpdatesVenueDB(updateVenue, request);
                                        response.Data = _mapper.Map<VenueRESPONSE>(savedVenue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                response.isError = true;
                response.statusCode = ex.StatusCode;
                response.ErrorMessage = ex.ErrorMessage;
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
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenuesByVenueTypeID(int id)
        {
            ServiceResponse<List<VenueRESPONSE>> result = new();

            try
            {
                if(await _helper.CheckVenueTypeId(id))
                {
                    result.Data = _mapper.Map<List<VenueRESPONSE>>(await _repo.GetAllVenuesByVenueTypeID(id));
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Venue_City>>> GetAllVenueCities()
        {
            ServiceResponse<List<Venue_City>> result = new();

            try
            {
                result.Data = await _repo.GetAllVenueCities();
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<FilterRESPONSE<VenueRESPONSE>>> FilterVenues(VenueFilterREQUEST request)
        {
            ServiceResponse<FilterRESPONSE<VenueRESPONSE>> result = new();

            try
            {

                var requestSend = new BaseFilterREQUEST
                {
                    ItemsPerPage = request.ItemsPerPage,
                    PageNumber = request.PageNumber,
                };


                var response = _helper.Paginator(request, _mapper.Map<List<VenueRESPONSE>>(await _repo.VenuesFilter(request)));

                //var response = await _repo.VenuesFilterTest(request);

                result.Data = response;

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<List<VenueRESPONSE>>> GetAllVenuesByManager(int managerId)
        {
            ServiceResponse<List<VenueRESPONSE>> result = new();

            try
            {
                var venues = await _repo.GetAllVenuesByManager(managerId);

                result.Data = _mapper.Map<List<VenueRESPONSE>>(venues);
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }
    }
}