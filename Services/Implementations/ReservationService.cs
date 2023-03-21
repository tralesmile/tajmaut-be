using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using System.Net;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IHelperValidationClassService _helper;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository repo, IHelperValidationClassService helper, IMapper mapper)
        {
            _repo = repo;
            _helper = helper;
            _mapper = mapper;
        }

        //create reservation
        public async Task<ServiceResponse<ReservationRESPONSE>> CreateReservation(ReservationREQUEST request)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {

                //if restaurant exists
                if (await _helper.CheckIdRestaurant(request.RestaurantId))
                {
                    //if event exists
                    if (await _helper.CheckIdEvent(request.EventId))
                    {
                        //is event is canceled
                        if (await _helper.CheckIdEventActivity(request.EventId))
                        {
                            //if event ended
                            if (await _helper.CheckIdEventDate(request.EventId))
                            {
                                //validate phone
                                if (_helper.ValidatePhoneRegex(request.Phone))
                                {
                                    //check fullname property
                                    if (_helper.ValidateEmailRegex(request.Email))
                                    {
                                        //check number of guests
                                        if (request.NumberGuests > 0)
                                        {

                                            var currentUserID = _helper.GetMe();

                                            var currentUserRole = _helper.GetCurrentUserRole();
                                            //check if current user is the entered user || role is admin,manager
                                            if (request.UserId == currentUserID || _helper.CheckUserAdminOrManager())
                                            {
                                                var resultSend = await _repo.CreateReservation(request);

                                                result.Data = _mapper.Map<ReservationRESPONSE>(resultSend);
                                            }
                                            else
                                            {
                                                throw new CustomError(401, $"Unauthorized User");
                                            }

                                        }
                                        else
                                        {
                                            throw new CustomError(401, $"Invalid number of guests field!");
                                        }
                                    }
                                }
                            }
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

        //delete reservation
        public async Task<ServiceResponse<ReservationRESPONSE>> DeleteReservation(int reservationId)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {

                //if reservation exists
                if (await _repo.ReservationExistsID(reservationId))
                {
                    //get currnet ID,Role,Reservation
                    var currentUserID = _helper.GetMe();
                    var currentUserRole = _helper.GetCurrentUserRole();
                    var currentReservation = await _repo.GetReservationByID(reservationId);

                    //current user id from token is the resevation's userid or admin,manager
                    if (currentUserID == currentReservation.UserId || _helper.CheckUserAdminOrManager())
                    {
                        //delete reservation
                        var check = await _repo.DeleteReservation(currentReservation);
                        if (check)
                        {
                            result.Data = _mapper.Map<ReservationRESPONSE>(currentReservation);
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

        //all reservations
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetAllReservations()
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {

                //check if current user is admin
                if (_helper.CheckUserAdmin())
                {
                    var listReservations = await _repo.GetAllReservations();

                    //if there are reservations
                    if (listReservations.Count() > 0)
                    {
                        result.Data = _mapper.Map<List<ReservationRESPONSE>>(listReservations);
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

        //all reservations by event 
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId)
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {
                //if the id is valid
                if (_helper.ValidateId(eventId))
                {
                    //admin or manager can access the reservations on specific event
                    if (_helper.CheckUserAdminOrManager())
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
                                    result.Data = _mapper.Map<List<ReservationRESPONSE>>(eventReservations);
                                }
                                else
                                {
                                    throw new CustomError(404, $"This event has no reservations");
                                }
                            }
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

        //all reservations by restaurant
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByRestaurant(int restaurantId)
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {
                //if restaurantId is valid
                if (_helper.ValidateId(restaurantId))
                {
                    //admin and manager access
                    if (_helper.CheckUserAdminOrManager())
                    {
                        //if restaurant exists
                        if (await _helper.CheckIdRestaurant(restaurantId))
                        {
                            var listReservations = await _repo.GetAllReservations();

                            if (listReservations.Count() > 0)
                            {
                                //search restaurants with that id
                                var restaurantReservations = listReservations.Where(n => n.RestaurantId == restaurantId).ToList();
                                if (restaurantReservations.Count() > 0)
                                {
                                    result.Data = _mapper.Map<List<ReservationRESPONSE>>(restaurantReservations);
                                }
                                else
                                {
                                    throw new CustomError(404, $"Restaurant has no reservations");
                                }
                            }
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

        //all reservations by user - filter exception
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByUser(int userId)
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {

                //if invalid userId
                if (userId < 0)
                    throw new CustomError(400, "Invalid UserId");

                //if user exists
                if (await _helper.CheckIdUser(userId))
                {

                    var currentUserID = _helper.GetMe();

                    //current user can access its reservations and admin,manager
                    if (userId == currentUserID || _helper.CheckUserAdminOrManager())
                    {
                        var allReservations = await _repo.GetAllReservations();
                        if (allReservations.Count() > 0)
                        {
                            //search for user's reservations
                            var userReservations = allReservations.Where(us => us.UserId == userId).ToList();

                            if (userReservations.Count() > 0)
                            {
                                result.Data = _mapper.Map<List<ReservationRESPONSE>>(userReservations);
                            }
                            else
                            {
                                throw new CustomError(404, "This user has no reservations!");
                            }
                        }
                    }
                    else
                    {
                        throw new CustomError(401, "Unauthorized User");
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

        public async Task<ServiceResponse<ReservationRESPONSE>> ManagerStatusReservation(int reservationId)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {

                var checkReservation = await _helper.CheckIdReservation(reservationId);

                if (checkReservation != null)
                {
                    if (await _repo.ChangeReservationStatus(checkReservation))
                        result.Data = _mapper.Map<ReservationRESPONSE>(checkReservation);
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
    }
}
