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
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Phone = user.Phone,
                City = user.City,
            };
            _ctx.Users.Add(newUser);
            await _ctx.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _ctx.Users.FindAsync(id);
            if (user != null)
            {
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }

        public async Task<User> UpdateUserAsync(UserPOST request, int id)
        {
            var user = await _ctx.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = request.Email;
                user.Password = request.Password;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Address = request.Address;
                user.Phone = request.Phone;
                user.City = request.City;
                await _ctx.SaveChangesAsync();
                return user;
            }
            return null;
        }
    }
}
