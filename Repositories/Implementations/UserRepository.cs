using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helperClass;
        public UserRepository(tajmautDataContext ctx, IHelperValidationClassService helperClass)
        {
            _ctx = ctx;
            _helperClass = helperClass;
        }

        //create user
        public async Task<User> CreateUserAsync(UserPostREQUEST request)
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
                ModifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
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
            if (user != null)
            {
                return user;
            }

            throw new CustomError(404, $"User not found");
        }

        //get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            //get all users to list
            var check = await _ctx.Users.ToListAsync();
            if (check != null)
            {
                return check;
            }

            throw new CustomError(404, $"No users found");
        }

        //get user by id
        public async Task<User> GetUserByIdAsync(int id)
        {
            //search for user
            var check = await _ctx.Users.FirstOrDefaultAsync(user => user.UserId == id);
            if (check != null)
            {
                return check;
            }

            throw new CustomError(404, $"User not found");
        }

        //save to database
        public async Task<User> AddEntity(User user)
        {
            _ctx.Users.Add(user);

            await _ctx.SaveChangesAsync();

            return user;
        }

        //update user
        public async Task<User> UpdateUserAsync(UserPutREQUEST request, int id)
        {
            //search for user
            var user = await _ctx.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            throw new CustomError(404, $"User not found");
        }

        //save changes
        public async Task<User> SaveChanges(User user, UserPutREQUEST request)
        {
            var currentUserID = _helperClass.GetMe();
            //create new user
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.ModifiedAt = DateTime.Now;
            user.ModifiedBy = currentUserID;

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

        //check old with new password
        public async Task<bool> CheckOldPassword(string oldPassword, int id)
        {
            var currentUser = await _ctx.Users.FindAsync(id);
            if (currentUser == null || !VerifyPasswordHash(oldPassword, currentUser.PasswordHash, currentUser.PasswordSalt))
            {
                throw new CustomError(400, $"Invalid old password");
            }
            return true;
        }

        //verifying hashed password
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //update password
        public async Task<User> UpdatePassword(User user, string newPassword)
        {
            //hash password
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            var currentUserID = _helperClass.GetMe();
            user.ModifiedAt = DateTime.UtcNow;
            user.ModifiedBy = currentUserID;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _ctx.SaveChangesAsync();
            return user;
        }
    }
}
