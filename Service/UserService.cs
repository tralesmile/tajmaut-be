using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IHelperValidationClassService _helperClass;

        public UserService(IUserRepository repo,IHelperValidationClassService helperClass)
        {
            _repo = repo;
            _helperClass = helperClass;
        }
        public async Task<User> CreateUserAsync(UserPostREQUEST user)
        {
            //get user from repo
            var getUser = await _repo.CreateUserAsync(user);

            //check for duplicates with a method that saves data
            var checkUser = await _helperClass.CheckDuplicatesEmail(getUser.Email);

            //check email and phone Regex
            if (_helperClass.ValidateEmailRegex(getUser.Email))
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

        public async Task<User> UpdateUserAsync(UserPostREQUEST request, int id)
        {
            //get result from repo
            var getUser = await _repo.UpdateUserAsync(request,id);

            //check if there is any
            if (getUser != null)
            {
                //check for duplicates
                var checkUser = await _helperClass.CheckDuplicatesEmailWithId(request.Email,getUser.UserId);

                //check email and phone
                if (_helperClass.ValidateEmailRegex(request.Email))
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
