using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using System.Net;
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

        //create reservation
        public async Task<ReservationRESPONSE> CreateReservation(ReservationREQUEST request)
        {
            //if request is null
            if(request == null)
            {
                throw new CustomBadRequestException($"Invalid input - NULL");
            }
            //if restaurant exists
            if(await _helper.CheckIdRestaurant(request.RestaurantId))
            {
                //if event exists
                if(await _helper.CheckIdEvent(request.EventId)) 
                {
                    //is event is canceled
                    if(await _helper.CheckIdEventActivity(request.EventId))
                    {
                        //if event ended
                        if(await _helper.CheckIdEventDate(request.EventId))
                        {
                            //validate phone
                            if(_helper.ValidatePhoneRegex(request.Phone))
                            {
                                //check fullname property
                                if(_helper.ValidateEmailRegex(request.Email))
                                {
                                    //check number of guests
                                    if(request.NumberGuests>0)
                                    {

                                        var currentUserID = _helper.GetMe();

                                        var currentUserRole = _helper.GetCurrentUserRole();
                                        //check if current user is the entered user || role is admin,manager
                                        if (request.UserId == currentUserID || currentUserRole == "Admin" || currentUserRole == "Manager")
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
                                    throw new CustomBadRequestException($"Invalid email!");
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

        //delete reservation
        public async Task<string> DeleteReservation(int reservationId)
        {
            //if reservation exists
            if(await _repo.ReservationExistsID(reservationId))
            {
                //get currnet ID,Role,Reservation
                var currentUserID = _helper.GetMe();
                var currentUserRole = _helper.GetCurrentUserRole();
                var currentReservation = await _repo.GetReservationByID(reservationId);

                //current user id from token is the resevation's userid or admin,manager
                if(currentUserID==currentReservation.UserId || currentUserRole=="Admin" || currentUserRole=="Manager")
                {
                    //delete reservation
                    var check = await _repo.DeleteReservation(currentReservation);
                    if(check)
                    {
                        return "Reservation Deleted";
                    }
                }
                else
                {
                    throw new CustomUnauthorizedException("Unauthorized USER");
                }
            }
            throw new CustomNotFoundException("Reservation Not Found");
        }

        //all reservations
        public async Task<List<ReservationRESPONSE>> GetAllReservations()
        {
            var currentUserRole = _helper.GetCurrentUserRole();

            if(currentUserRole == "Admin")
            {
                var listReservations = await _repo.GetAllReservations();

                //if there are reservations
                if(listReservations.Count() > 0) 
                {
                    return _mapper.Map<List<ReservationRESPONSE>>(listReservations);
                }
                else
                {
                    throw new CustomNotFoundException($"No reservations found");
                }
            }
            throw new CustomUnauthorizedException($"Unauthorized User");
        }

        //all reservations by event - service response
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId)
        {
            //service response
            var serviceResponse = new ServiceResponse<List<ReservationRESPONSE>>();
            serviceResponse.Data = null;
            serviceResponse.Success = false;

            //if the id is valid
            if (eventId>0)
            {
                var currentUserRole = _helper.GetCurrentUserRole();
                //admin or manager can access the reservations on specific event
                if (currentUserRole == "Admin" || currentUserRole == "Manager")
                {
                    //if event exists
                    if (await _helper.CheckIdEvent(eventId))
                    {
                        var listReservations = await _repo.GetAllReservations();

                        if (listReservations.Count() > 0)
                        {
                            //search event reservations with that id
                            var eventReservations = listReservations.Where(e => e.EventId == eventId).ToList();

                            if (eventReservations.Count() > 0)
                            {
                                var responseData =  _mapper.Map<List<ReservationRESPONSE>>(eventReservations);
                                serviceResponse.Data = responseData;
                                serviceResponse.Success = true;
                                serviceResponse.Message = $"Success";
                                serviceResponse.StatusCode = HttpStatusCode.OK;
                                return serviceResponse;
                            }
                            else
                            {
                                serviceResponse.Message = $"This event has no reservations";
                                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                                return serviceResponse;
                            }
                        }
                        else
                        {
                            serviceResponse.Message = $"No reservations found";
                            serviceResponse.StatusCode = HttpStatusCode.NotFound;
                            return serviceResponse;
                        }
                    }
                    else
                    {
                        serviceResponse.Message = $"Event Not Found";
                        serviceResponse.StatusCode = HttpStatusCode.NotFound;
                        return serviceResponse;
                    }
                }
                else
                {
                    serviceResponse.Message = $"Unauthorized User";
                    serviceResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return serviceResponse;
                }
            }
            serviceResponse.Message = $"Invalid ID input";
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            return serviceResponse;
            
        }

        //all reservations by restaurant
        public async Task<List<ReservationRESPONSE>> GetReservationsByRestaurant(int restaurantId)
        {
            //if restaurantId is valid
            if(restaurantId>0)
            {
                var currentUserRole= _helper.GetCurrentUserRole();
                //admin and manager access
                if(currentUserRole=="Admin" || currentUserRole=="Manager")
                {
                    //if restaurant exists
                    if (await _helper.CheckIdRestaurant(restaurantId))
                    {
                        var listReservations = await _repo.GetAllReservations();

                        if(listReservations.Count() > 0)
                        {
                            //search restaurants with that id
                            var restaurantReservations = listReservations.Where(n=>n.RestaurantId== restaurantId).ToList();
                            if(restaurantReservations.Count()>0)
                            {
                                return _mapper.Map<List<ReservationRESPONSE>>(restaurantReservations);
                            }
                            else
                            {
                                throw new CustomNotFoundException("Restaurant has no reservations");
                            }
                        }
                        else
                        {
                            throw new CustomNotFoundException("No reservations found");
                        }
                    }
                    else
                    {
                        throw new CustomNotFoundException("Restaurant Not Found!");
                    }
                }
                else
                {
                    throw new CustomUnauthorizedException("Unauthorized User");
                }
            }
            throw new CustomBadRequestException("Invalid Restaurant ID");
        }

        //all reservations by user
        public async Task<List<ReservationRESPONSE>> GetReservationsByUser(int userId)
        {
            //if invalid userId
            if (userId < 0)
                throw new CustomBadRequestException("Invalid UserId");

            //if user exists
            if(await _helper.CheckIdUser(userId))
            {
                var currentUserID = _helper.GetMe();
                var currentUserRole = _helper.GetCurrentUserRole();
                //current user can access its reservations and admin,manager
                if (userId == currentUserID || currentUserRole == "Admin" || currentUserRole == "Manager")
                {
                    var allReservations = await _repo.GetAllReservations();

                    if (allReservations.Count() > 0 || allReservations != null)
                    {
                        //search for user's reservations
                        var userReservations = allReservations.Where(us=>us.UserId== userId).ToList();

                        if(userReservations.Count()>0)
                        {
                            return _mapper.Map<List<ReservationRESPONSE>>(userReservations);
                        }
                        else
                        {
                            throw new CustomNotFoundException("This user has no reservations!");
                        }

                    }
                    else
                    {
                        throw new CustomNotFoundException("No reservations found!");
                    }
                }
                else
                {
                    throw new CustomUnauthorizedException("Unauthorized User");
                }
            }
            throw new CustomNotFoundException("User Not Found!");
        }

        public async Task<ReservationRESPONSE> ManagerStatusReservation(int reservationId)
        {
            var checkReservation = await _helper.CheckIdReservation(reservationId);
            if(checkReservation != null)
            {
                if (await _repo.ChangeReservationStatus(checkReservation))
                    return _mapper.Map<ReservationRESPONSE>(checkReservation);
            }
            throw new CustomNotFoundException($"Reservation not found");
        }
    }
}
