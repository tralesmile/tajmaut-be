using AutoMapper;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Repository.Interfaces;


namespace TajmautMK.Core.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IHelperValidationClassService _helper;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMailService;
        private readonly ISendMailRepository _sendMailRepo;

        public ReservationService(IReservationRepository repo, IHelperValidationClassService helper, IMapper mapper, ISendMailRepository sendMailRepo,ISendMailService sendMailService)
        {
            _repo = repo;
            _helper = helper;
            _mapper = mapper;
            _sendMailRepo = sendMailRepo;
            _sendMailService = sendMailService;
        }

        //create reservation
        public async Task<ServiceResponse<ReservationRESPONSE>> CreateReservation(ReservationREQUEST request)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {

                //if restaurant exists
                if (await _helper.CheckIdVenue(request.VenueId))
                {
                    //if event exists
                    if (await _helper.CheckIdEvent(request.EventId))
                    {
                        //if event is in that venue
                        if (await _helper.CheckEventVenueRelation(request.VenueId, request.EventId))
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
                                                    if (await _repo.CheckNumReservations(request))
                                                    {
                                                        var resultSend = await _repo.CreateReservation(request);

                                                        result.Data = _mapper.Map<ReservationRESPONSE>(resultSend);
                                                    }
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

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //delete reservation
        public async Task<ServiceResponse<ReservationRESPONSE>> DeleteReservation(int reservationId)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {
                var currentUserID = _helper.GetMe();
                var reservationByID = await _repo.GetReservationByID(reservationId);
                var venueID = reservationByID.VenueId;

                //if reservation exists
                if (await _repo.ReservationExistsID(reservationId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                    {
                        //get currnet ID,Role,Reservation
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
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
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
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //all reservations by event 
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByEvent(int eventId)
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {
                var currentUserID = _helper.GetMe();
                var eventbyID = await _helper.GetEventByID(eventId);
                var venueID = eventbyID.VenueId;

                //if the id is valid
                if (_helper.ValidateId(eventId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
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
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;

        }

        //all reservations by restaurant
        public async Task<ServiceResponse<List<ReservationRESPONSE>>> GetReservationsByVenue(int venueId)
        {

            ServiceResponse<List<ReservationRESPONSE>> result = new();

            try
            {
                var currentUserID = _helper.GetMe();

                //if restaurantId is valid
                if (_helper.ValidateId(venueId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueId, currentUserID))
                    {
                        //admin and manager access
                        if (_helper.CheckUserAdminOrManager())
                        {
                            //if restaurant exists
                            if (await _helper.CheckIdVenue(venueId))
                            {
                                var listReservations = await _repo.GetAllReservations();

                                if (listReservations.Count() > 0)
                                {
                                    //search restaurants with that id
                                    var venueReservations = listReservations.Where(n => n.VenueId == venueId).ToList();
                                    if (venueReservations.Count() > 0)
                                    {
                                        result.Data = _mapper.Map<List<ReservationRESPONSE>>(venueReservations);
                                    }
                                    else
                                    {
                                        throw new CustomError(404, $"Venue has no reservations");
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
                result.ErrorMessage = ex.ErrorMessage;
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
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<ReservationRESPONSE>> ManagerStatusReservation(int reservationId)
        {

            ServiceResponse<ReservationRESPONSE> result = new();

            try
            {
                var currentUserID = _helper.GetMe();
                var reservationByID = await _repo.GetReservationByID(reservationId);
                var venueID = reservationByID.VenueId;

                if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                {
                    if (await _helper.CheckIdEventActivity(reservationByID.EventId))
                    {
                        if (await _helper.CheckIdEventDate(reservationByID.EventId))
                        {
                            if (await _repo.ChangeReservationStatus(reservationByID))
                            {
                                //1.Mail template
                                var template = _sendMailRepo.ConfirmReservationTemplate(reservationByID);

                                //2.Send mail
                                var mailSend = new MailSendREQUEST
                                {
                                    Template = template,
                                    To = reservationByID.Email,
                                    Subject = "Детали за резервација",
                                };

                                var message = _sendMailRepo.MailSender(mailSend);

                                result.Data = _mapper.Map<ReservationRESPONSE>(reservationByID);
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
    }
}
