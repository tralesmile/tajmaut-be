using Azure.Core;
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

        //check duplicates
        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        //create user
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

        //delete from db and save changes
        public async Task<User> DeleteEntity(User user)
        {
            //removing user
            _ctx.Users.Remove(user);

            //saving changes
            await _ctx.SaveChangesAsync();

            return user;
        }

        //delete user
        public async Task<User> DeleteUserAsync(int id)
        {
            //find user
            var user = await _ctx.Users.FindAsync(id);
            
            return user;
        }

        //get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            //get all users to list
            return await _ctx.Users.ToListAsync();
        }
        
        //get user by id
        public async Task<User> GetUserByIdAsync(int id)
        {
            //search for user
            return await _ctx.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }

        //save to database
        public async Task<User> AddEntity(User user)
        {
            _ctx.Users.Add(user);

            await _ctx.SaveChangesAsync();

            return user;
        }

        //update user
        public async Task<User> UpdateUserAsync(UserPOST request, int id)
        {
            //search for user
            var user = await _ctx.Users.FindAsync(id);

            return user;
        }

        //save changes
        public async Task<User> SaveChanges(User user, UserPOST request)
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
    }
}
