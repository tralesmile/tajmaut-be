using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace tajmautAPI.Helper
{
    public class HelperValidationClassService : IHelperValidationClassService
    {

        private readonly IHelperValidationClassRepository _helperRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HelperValidationClassService(IHelperValidationClassRepository helperRepo, IHttpContextAccessor httpContextAccessor)
        {
            _helperRepo = helperRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        //check duplicates email
        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _helperRepo.CheckDuplicatesEmail(email);
        }

        //check duplicates of email
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            return await _helperRepo.CheckDuplicatesEmailWithId(email, id);
        }

        //check if category exists
        public async Task<bool> CheckIdCategory(int id)
        {
            return await _helperRepo.CheckIdCategory(id);
        }

        //check if restaurant exists
        public async Task<bool> CheckIdVenue(int id)
        {
            return await _helperRepo.CheckIdVenue(id);
        }

        //validate email
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

        //get current user id
        public int GetMe()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return int.Parse(result);
        }

        //get currnet user email from token
        public string GetCurrentUserEmail()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        //get current user role from token
        public string GetCurrentUserRole()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }

        //check if events exists
        public async Task<bool> CheckIdEvent(int id)
        {
            return await _helperRepo.CheckIdEvent(id);
        }

        //check event activity
        public async Task<bool> CheckIdEventActivity(int id)
        {
            return await _helperRepo.CheckIdEventActivity(id);
        }

        //check date event
        public async Task<bool> CheckIdEventDate(int id)
        {
            return await _helperRepo.CheckIdEventDate(id);
        }

        //validate phone
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

        //check if user exists
        public async Task<bool> CheckIdUser(int id)
        {
            bool result = await _helperRepo.CheckIdUser(id);
            if (result)
            {
                return true;
            }

            throw new CustomError(404, $"User not found");
        }

        //check if reservations exists
        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            return await _helperRepo.CheckIdReservation(id);
        }

        //current user admin?
        public bool CheckUserAdmin()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin")
                return true;

            throw new CustomError(401, $"Current user is not Admin");
        }

        //user or manager current user
        public bool CheckUserAdminOrManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin" || check == "Manager")
            {
                return true;
            }

            throw new CustomError(401, $"Current user is not Admin or Manager");
        }

        //check if current user is manager
        public bool CheckUserManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Manager")
                return true;

            throw new CustomError(401, $"Current user is not Manager");
        }

        //validate id input
        public bool ValidateId(int id)
        {
            if (id > 0)
                return true;

            throw new CustomError(400, $"Invalid ID");
        }

        //check if comment exists
        public async Task<bool> CheckIdComment(int commentId)
        {
            return await _helperRepo.CheckIdComment(commentId);
        }

        //get comment with id
        public async Task<Comment> GetCommentId(int id)
        {
            return await _helperRepo.GetCommentId(id);
        }

        public async Task<User> GetUserWithEmail(string email)
        {
            return await _helperRepo.GetUserWithEmail(email);
        }

        public async Task<bool> CheckEventVenueRelation(int venueId, int eventId)
        {
            return await _helperRepo.CheckEventVenueRelation(venueId, eventId);
        }

        public async Task<bool> CheckVenueTypeId(int id)
        {
            return await _helperRepo.CheckIdVenue(id);
        }
    }
}
