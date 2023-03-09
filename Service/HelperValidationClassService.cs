using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

namespace tajmautAPI.Service
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

        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _helperRepo.CheckDuplicatesEmail(email);
        }

        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            return await _helperRepo.CheckDuplicatesEmailWithId(email, id);
        }

        public async Task<bool> CheckIdCategory(int id)
        {
            return await (_helperRepo.CheckIdCategory(id));
        }

        public async Task<bool> CheckIdRestaurant(int id)
        {
            return await _helperRepo.CheckIdRestaurant(id);
        }

        public bool ValidateEmailRegex(string emailRegex)
        {

            //validate email with regex
            string pattern = @"^[a-zA-Z0-9._%+-]+@(hotmail|yahoo|gmail|outlook)\.(com|net|org|mk)$";
            bool isValidEmail = Regex.IsMatch(emailRegex, pattern);

            if (isValidEmail)
            {
                return true;
            }

            throw new CustomException(HttpStatusCode.BadRequest, $"Invalid Email");
        }

        public int GetMe()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return int.Parse(result);
        }

        public string GetCurrentUserEmail()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        public string GetCurrentUserRole()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }

        public async Task<bool> CheckIdEvent(int id)
        {
            return await _helperRepo.CheckIdEvent(id);
        }

        public async Task<bool> CheckIdEventActivity(int id)
        {
            return await _helperRepo.CheckIdEventActivity(id);
        }

        public async Task<bool> CheckIdEventDate(int id)
        {
            return await _helperRepo.CheckIdEventDate(id);
        }

        public bool ValidatePhoneRegex(string phone)
        {
            string pattern = @"^(?:07[0-9]|08[0]|080)[0-9]{6}$";
            bool isValidPhone = Regex.IsMatch(phone, pattern);

            if (isValidPhone)
            {
                return true;
            }
            throw new CustomException(HttpStatusCode.BadRequest, $"Invalid phone number!");
        }

        public async Task<bool> CheckIdUser(int id)
        {
            bool result=  await _helperRepo.CheckIdUser(id);
            if(result)
            {
                return true;
            }

            throw new CustomException(HttpStatusCode.NotFound, $"User not found");
        }

        public async Task<OnlineReservation> CheckIdReservation(int id)
        {
            return await _helperRepo.CheckIdReservation(id);
        }

        public bool CheckUserAdmin()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin")
                return true;

            throw new CustomException(HttpStatusCode.Unauthorized, $"Current user is not Admin");
        }

        public bool CheckUserAdminOrManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Admin" || check == "Manager")
            {
                return true;
            }

            throw new CustomException(HttpStatusCode.Unauthorized, $"Current user is not Admin or Manager");
        }

        public bool CheckUserManager()
        {
            string check = GetCurrentUserRole();
            if (check == "Manager")
                return true;

            throw new CustomException(HttpStatusCode.Unauthorized, $"Current user is not Manager");
        }

        public bool ValidateId(int id)
        {
            if (id > 0)
                return true;

            throw new CustomException(HttpStatusCode.BadRequest, $"Invalid ID");
        }
    }
}
