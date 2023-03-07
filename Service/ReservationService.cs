using AutoMapper;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IHelperValidationClassService _helper;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository repo, IHelperValidationClassService helper,IMapper mapper)
        {
            _repo = repo;
            _helper = helper;
            _mapper = mapper;
        }

        public async Task<ReservationRESPONSE> CreateReservation(ReservationREQUEST request)
        {
            if(request == null)
            {
                throw new CustomBadRequestException($"Invalid input - NULL");
            }
            if(await _helper.CheckIdRestaurant(request.RestaurantId))
            {
                if(await _helper.CheckIdEvent(request.EventId)) 
                {
                    if(await _helper.CheckIdEventActivity(request.EventId))
                    {
                        if(await _helper.CheckIdEventDate(request.EventId))
                        {
                            if(_helper.ValidatePhoneRegex(request.Phone))
                            {
                                if(request.FullName!="" || request.FullName!=null)
                                {
                                    if(request.NumberGuests>0)
                                    {
                                        var currentUserID = _helper.GetMe();
                                        var currentUserRole = _helper.GetCurrentUserRole();
                                        if (request.UserId == currentUserID || request.UserId!=0 || currentUserRole == "Admin" || currentUserRole == "Manager")
                                        {
                                            var result = await _repo.CreateReservation(request);
                                            return _mapper.Map<ReservationRESPONSE>(result);
                                        }
                                        else
                                        {
                                            throw new CustomUnauthorizedException($"Unauthorized User");
                                        }

                                    }
                                    else
                                    {
                                        throw new CustomBadRequestException($"Invalid number of guests field!");
                                    }
                                }
                                else
                                {
                                    throw new CustomBadRequestException($"Invalid name!");
                                }
                            }
                            else
                            {
                                throw new CustomBadRequestException($"Invalid phone number!");
                            }
                        }
                        else
                        {
                            throw new CustomBadRequestException($"Event ended!");
                        }
                    }
                    else
                    {
                        throw new CustomBadRequestException($"Event was canceled");
                    }
                }
                else
                {
                    throw new CustomNotFoundException($"Event not found!");
                }
            }
            else
            {
                throw new CustomNotFoundException($"Restaurant not found!");
            }

            throw new CustomBadRequestException($"Invalid input");
        }
    }
}
