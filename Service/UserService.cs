using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
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

            //checking for duplicates
            if (checkUser == null)
            {
                return await _repo.AddEntity(getUser);
            }
            else
            {
                return null;
            }


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
                //check for duplicates with a method that saves data
                var checkUser = await _repo.CheckDuplicatesEmail(request.Email);

                //checking for duplicates
                if (checkUser == null)
                {
                    //update the user 
                    return await _repo.SaveChanges(getUser, request);
                }
            }
                return null;
        }
    }
}
