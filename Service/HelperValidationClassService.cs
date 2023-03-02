using System.Security.Claims;
using System.Text.RegularExpressions;
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

            return false;
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
    }
}
