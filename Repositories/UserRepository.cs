﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using tajmautAPI.Data;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helperClass;
        public UserRepository(tajmautDataContext ctx,IHelperValidationClassService helperClass)
        {
            _ctx= ctx;
            _helperClass= helperClass;
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
                ModifiedAt= DateTime.Now,
                CreatedAt= DateTime.Now,
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
        public async Task<User> UpdateUserAsync(UserPostREQUEST request, int id)
        {
            //search for user
            var user = await _ctx.Users.FindAsync(id);

            return user;
        }

        //save changes
        public async Task<User> SaveChanges(User user, UserPostREQUEST request)
        {
            //hash the password
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var currentUserID = _helperClass.GetMe();
            //create new user
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.FirstName= request.FirstName;
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
    }
}
