using System.Security.Claims;
using System.Text.RegularExpressions;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.API.Helper
{
    /// <summary>
    /// Service for performing validation checks and getting information related to users, events, and reservations.
    /// </summary>
    public class HelperValidationClassService : IHelperValidationClassService
    {

        private readonly IHelperValidationClassRepository _helperRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the HelperValidationClassService class.
        /// </summary>
        /// <param name="helperRepo">An instance of the IHelperValidationClassRepository interface.</param>
        /// <param name="httpContextAccessor">An instance of the IHttpContextAccessor interface.</param>
        public HelperValidationClassService(IHelperValidationClassRepository helperRepo, IHttpContextAccessor httpContextAccessor)
        {
            _helperRepo = helperRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Checks if an email already exists in the database.
        /// </summary>
        /// <param name="email">The email to check for duplicates.</param>
        /// <returns>A User object if the email is already in use, otherwise null.</returns>
        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _helperRepo.CheckDuplicatesEmail(email);
        }

        /// <summary>
        /// Checks if an email already exists in the database, excluding the user with the specified ID.
        /// </summary>
        /// <param name="email">The email to check for duplicates.</param>
        /// <param name="id">The ID of the user to exclude from the search.</param>
        /// <returns>A User object if the email is already in use by another user, otherwise null.</returns>
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            return await _helperRepo.CheckDuplicatesEmailWithId(email, id);
        }

        /// <summary>
        /// Checks if a category with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the category to check for.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public async Task<bool> CheckIdCategory(int id)
        {
            return await _helperRepo.CheckIdCategory(id);
        }

        /// <summary>
        /// Checks if a venue with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the venue to check for.</param>
        /// <returns>True if the venue exists, otherwise false.</returns>
        public async Task<bool> CheckIdVenue(int id)
        {
            return await _helperRepo.CheckIdVenue(id);
        }

        /// <summary>
        /// Validates an email address using a regular expression pattern.
        /// </summary>
        /// <param name="emailRegex">The email address to validate.</param>
        /// <returns>True if the email address is valid, otherwise throws a CustomError exception.</returns>
        public bool ValidateEmailRegex(string emailRegex)
        {

            //validate email with regex
            string pattern = @"^[a-zA-Z0-9._]{1,100}\@[a-zA-Z0-9.-]{2,10}\.[a-zA-Z]{2,6}$";
            bool isValidEmail = Regex.IsMatch(emailRegex, pattern);

            if (isValidEmail)
            {
                return true;
            }

            throw new CustomError(400, $"Invalid Email");
        }

        /// <summary>
        /// Gets the ID of the current user from the HttpContext.
        /// </summary>
        /// <returns>The ID of the current user.</returns>
        public int GetMe()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.Parse(result);
            }
            throw new CustomError(404, $"User not found");
        }

        /// <summary>
        /// Gets the email of the current user from the HttpContext.
        /// </summary>
        /// <returns>The email address of the current user.</returns>
        public string GetCurrentUserEmail()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        /// <summary>
        /// Gets the role of the current user from the HttpContext.
        /// </summary>
        /// <returns>The role of the current user.</returns>
        public string GetCurrentUserRole()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }

        /// <summary>
        /// Checks if an event with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the event to check for.</param>
        /// <returns>True if the event exists, otherwise false.</returns>
        public async Task<bool> CheckIdEvent(int id)
        {
            return await _helperRepo.CheckIdEvent(id);
        }

        /// <summary>
        /// Checks if the specified event is canceled.
        /// </summary>
        /// <param name="id">The ID of the event activity to check.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<bool> CheckIdEventActivity(int id)
        {
            return await _helperRepo.CheckIdEventActivity(id);
        }

        /// <summary>
        /// Checks if the specified event ended or not.
        /// </summary>
        /// <param name="id">The ID of the event date to check.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<bool> CheckIdEventDate(int id)
        {
            return await _helperRepo.CheckIdEventDate(id);
        }

        /// <summary>
        /// Validates a phone number using a regular expression.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>A Boolean value indicating whether the phone number is valid.</returns>
        /// <exception cref="CustomError">Thrown when the phone number is invalid.</exception>
        public bool ValidatePhoneRegex(string phone)
        {
            string pattern = @"^(?:07[0-9]|08[0]|080)[0-9]{6}$";
            bool isValidPhone = Regex.IsMatch(phone, pattern);

            if (isValidPhone)
            {
                return true;
            }
            throw new CustomError(400, $"Invalid phone number!");
        }

        /// <summary>
        /// Checks if the specified user exists in the database.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="CustomError">Thrown when the user is not found.</exception>
        public async Task<bool> CheckIdUser(int id)
        {
            bool result = await _helperRepo.CheckIdUser(id);
            if (result)
            {
                return true;
            }

            throw new CustomError(404, $"User not found");
        }

        /// <summary>
        /// Checks if the specified reservation exists in the database.
        /// </summary>
        /// <param name="id">The ID of the reservation to check.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            return await _helperRepo.CheckIdReservation(id);
        }

        /// <summary>
        /// Checks if the current user is an admin.
        /// </summary>
        /// <returns>A Boolean value indicating whether the current user is an admin.</returns>
        /// <exception cref="CustomError">Thrown when the current user is not an admin.</exception>
        public bool CheckUserAdmin()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin")
                return true;

            throw new CustomError(401, $"Current user is not Admin");
        }

        /// <summary>
        /// Checks if the current user is an admin or a manager.
        /// </summary>
        /// <returns>A Boolean value indicating whether the current user is an admin or a manager.</returns>
        /// <exception cref="CustomError">Thrown when the current user is not an admin or a manager.</exception>
        public bool CheckUserAdminOrManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin" || check == "Manager")
            {
                return true;
            }

            throw new CustomError(401, $"Current user is not Admin or Manager");
        }

        /// <summary>
        /// Checks if the current user is a manager.
        /// </summary>
        /// <returns>A Boolean value indicating whether the current user is a manager.</returns>
        /// <exception cref="CustomError">Thrown when the current user is not a manager.</exception>
        public bool CheckUserManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Manager")
                return true;

            throw new CustomError(401, $"Current user is not Manager");
        }

        /// <summary>
        /// Validates an ID input.
        /// </summary>
        /// <param name="id">The ID to validate.</param>
        /// <returns>A Boolean value indicating whether the ID is valid.</returns>
        /// <exception cref="CustomError">Thrown when the ID is invalid.</exception>
        public bool ValidateId(int id)
        {
            if (id > 0)
                return true;

            throw new CustomError(400, $"Invalid ID");
        }

        /// <summary>
        /// Checks if the specified comment exists in the database.
        /// </summary>
        /// <param name="commentId">The ID of the comment to check.</param>
        /// <returns>A task that represents the asynchronous operation. </returns>
        public async Task<bool> CheckIdComment(int commentId)
        {
            return await _helperRepo.CheckIdComment(commentId);
        }

        /// <summary>
        /// Retrieves a comment with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The comment with the specified ID.</returns>
        public async Task<Comment> GetCommentId(int id)
        {
            return await _helperRepo.GetCommentId(id);
        }

        /// <summary>
        /// Retrieves a user with the specified email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user with the specified email.</returns>
        public async Task<User> GetUserWithEmail(string email)
        {
            return await _helperRepo.GetUserWithEmail(email);
        }

        /// <summary>
        /// Checks if a venue is related to an event.
        /// </summary>
        /// <param name="venueId">The ID of the venue to check.</param>
        /// <param name="eventId">The ID of the event to check.</param>
        /// <returns>True if the venue is related to the event; otherwise, false.</returns>
        public async Task<bool> CheckEventVenueRelation(int venueId, int eventId)
        {
            return await _helperRepo.CheckEventVenueRelation(venueId, eventId);
        }

        /// <summary>
        /// Checks if a venue type ID exists.
        /// </summary>
        /// <param name="id">The ID of the venue type to check.</param>
        /// <returns>True if venue exists ,false if not.</returns>
        public async Task<bool> CheckVenueTypeId(int id)
        {
            return await _helperRepo.CheckVenueTypeId(id);
        }

        public async Task<bool> CheckManagerVenueRelation(int venueId, int managerId)
        {
            return await _helperRepo.CheckManagerVenueRelation(venueId, managerId);
        }

        public async Task<Event> GetEventByID(int eventId)
        {
            return await _helperRepo.GetEventByID(eventId);
        }

        public async Task<List<Venue>> GetVenuesByCityId(int cityId)
        {
            return await _helperRepo.GetVenuesByCityId(cityId);
        }

        public FilterRESPONSE<T> Paginator<T>(BaseFilterREQUEST request, List<T> items)
        {
            var totalItems = items.Count();
            var itemsPerPage = totalItems;
            var pageNumber = 1;

            if (items.Count() <= 0)
            {
                throw new CustomError(404, $"No events found");
            }

            if (request.ItemsPerPage.HasValue && request.PageNumber.HasValue)
            {
                itemsPerPage = request.ItemsPerPage.Value;
                pageNumber = request.PageNumber.Value;

                items = items
                    .Skip((pageNumber - 1) * (int)itemsPerPage)
                    .Take((int)itemsPerPage).ToList();
            }

            var response = new FilterRESPONSE<T>
            {
                Items = items,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems,
            };

            return response;
        }
    }
}
