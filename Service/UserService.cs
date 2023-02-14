using System.Runtime.InteropServices;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

namespace tajmautAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly tajmautDataContext _ctx;
        public UserService(IUserRepository repo, tajmautDataContext ctx)
        {
            _repo = repo;
            _ctx = ctx;
        }
        public async Task<User> CreateUserAsync(UserPOST user)
        {
            //get user from repo
            var getUser = await _repo.CreateUserAsync(user);
            //check for duplicates
            var checkUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == getUser.Email.ToLower());

            //checking for duplicates
            if (checkUser == null)
            {
                _ctx.Users.Add(getUser);
                await _ctx.SaveChangesAsync();
                return getUser;
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
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
                return user;
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
                getUser.Email = request.Email;
                getUser.Password = request.Password;
                getUser.FirstName = request.FirstName;
                getUser.LastName = request.LastName;
                getUser.Address = request.Address;
                getUser.Phone = request.Phone;
                getUser.City = request.City;
                await _ctx.SaveChangesAsync();
                return getUser;
            }
            else
            {
                return null;
            }
        }
    }
}
