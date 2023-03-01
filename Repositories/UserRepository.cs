using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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

        //check duplicates without the current user
        public async Task<User> CheckDuplicatesEmailWithId(string email,int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserId != id);
        }

        //create user
        public async Task<User> CreateUserAsync(UserPOST request)
        {

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //create new user
            return new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                //Password = request.Password,
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
            //hash the password
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //create new user
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.FirstName= request.FirstName;
            user.LastName = request.LastName;
            await _ctx.SaveChangesAsync();
            return user;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //password hash
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
