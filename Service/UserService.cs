using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

namespace tajmautAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<User> CreateUserAsync(UserPOST user)
        {
            //get user from repo
            var getUser = await _repo.CreateUserAsync(user);

            //check for duplicates with a method that saves data
            var checkUser = await _repo.CheckDuplicatesEmail(getUser.Email);

            //validate email with regex
            string pattern = @"^[a-zA-Z0-9._%+-]+@(hotmail|yahoo|gmail|outlook)\.(com|net|org|mk)$";
            bool isValidEmail = Regex.IsMatch(getUser.Email, pattern);

            //validate phone with regex
            string phonePattern = @"^(07[0-9]|080)[-\s]?\d{3}[-\s]?\d{3}$";
            bool isValidPhone = Regex.IsMatch(getUser.Phone, phonePattern);

            //check email and phone
            if (isValidPhone && isValidEmail)
            {
                //checking for duplicates
                if (checkUser == null)
                {
                    return await _repo.AddEntity(getUser);
                }
            }
            return null;

        }

        public async Task<User> DeleteUserAsync(int id)
        {
            //get result from repo
            var user = await _repo.DeleteUserAsync(id);

            //check if there is any
            if (user != null)
            {
                return await _repo.DeleteEntity(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repo.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repo.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(UserPOST request, int id)
        {
            //get result from repo
            var getUser = await _repo.UpdateUserAsync(request,id);

            //check if there is any
            if (getUser != null)
            {
                //check for duplicates
                var checkUser = await _repo.CheckDuplicatesEmailWithId(request.Email,getUser.UserId);

                //validate email with regex
                string pattern = @"^[a-zA-Z0-9._%+-]+@(hotmail|yahoo|gmail|outlook)\.(com|net|org|mk)$";
                bool isValidEmail = Regex.IsMatch(getUser.Email, pattern);

                //validate phone with regex
                string phonePattern = @"^(07[0-9]|080)\d{7}$";
                bool isValidPhone = Regex.IsMatch(getUser.Phone, pattern);

                //check email and phone
                if (isValidPhone && isValidEmail)
                {
                    //checking for duplicates
                    if (checkUser == null)
                    {
                        //update the user 
                        return await _repo.SaveChanges(getUser, request);
                    }
                }
            }
                return null;
        }

    }
}
