using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
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

        public async Task<string> DeleteReservation(int reservationId)
        {
            if(await _repo.ReservationExistsID(reservationId))
            {
                var currentUserID = _helper.GetMe();
                var currentUserRole = _helper.GetCurrentUserRole();
                var currentReservation = await _repo.GetReservationByID(reservationId);
                if(currentUserID==currentReservation.UserId || currentUserRole=="Admin" || currentUserRole=="Manager")
                {
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

        public async Task<List<ReservationRESPONSE>> GetAllReservations()
        {
            var currentUserRole = _helper.GetCurrentUserRole();
            if(currentUserRole == "Admin")
            {
                var listReservations = await _repo.GetAllReservations();
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

        public async Task<List<ReservationRESPONSE>> GetReservationsByEvent(int eventId)
        {
            if(eventId>0)
            {
                var currentUserRole = _helper.GetCurrentUserRole();
                if (currentUserRole == "Admin" || currentUserRole == "Manager")
                {
                    if (await _helper.CheckIdEvent(eventId))
                    {
                        var listReservations = await _repo.GetAllReservations();
                        if (listReservations.Count() > 0)
                        {
                            var eventReservations = listReservations.Where(e => e.EventId == eventId).ToList();
                            if (eventReservations.Count() > 0)
                            {
                                return _mapper.Map<List<ReservationRESPONSE>>(eventReservations);
                            }
                            else
                            {
                                throw new CustomNotFoundException("This event has no reservations");
                            }
                        }
                        else
                        {
                            throw new CustomNotFoundException("No reservations found");
                        }
                    }
                    else
                    {
                        throw new CustomNotFoundException("Event Not Found");
                    }
                }
                else
                {
                    throw new CustomUnauthorizedException("Unauthorized User");
                }
            }
            throw new CustomBadRequestException("Invalid event ID");
        }

        public async Task<List<ReservationRESPONSE>> GetReservationsByUser(int userId)
        {

            if (userId < 0)
                throw new CustomBadRequestException("Invalid UserId");

            if(await _helper.CheckIdUser(userId))
            {
                var currentUserID = _helper.GetMe();
                var currentUserRole = _helper.GetCurrentUserRole();
                if (userId == currentUserID || currentUserRole == "Admin" || currentUserRole == "Manager")
                {
                    var allReservations = await _repo.GetAllReservations();
                    if (allReservations.Count() > 0 || allReservations != null)
                    {
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
    }
}
