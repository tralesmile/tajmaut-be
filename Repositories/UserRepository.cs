using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tajmautAPI.Data;
using tajmautAPI.Interfaces;
using tajmautAPI.Models;


namespace tajmautAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly tajmautDataContext _ctx;
        public UserRepository(tajmautDataContext ctx)
        {
            _ctx= ctx;
        }
        public async Task<User> CreateUserAsync(UserPOST user)
        {

            //create new user
            return new User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Phone = user.Phone,
                City = user.City,
            };

        }

        public async Task<User> DeleteUserAsync(int id)
        {
            //find user
            var user = await _ctx.Users.FindAsync(id);
            
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            //get all users to list
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            //search for user
            return await _ctx.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }

        public async Task<User> UpdateUserAsync(UserPOST request, int id)
        {
            //search for user
            var user = await _ctx.Users.FindAsync(id);

            return user;
        }
    }
}
