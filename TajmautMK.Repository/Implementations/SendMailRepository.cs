using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Data;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class SendMailRepository : ISendMailRepository
    {

        private readonly tajmautDataContext _ctx;

        public SendMailRepository(tajmautDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                return user;
            }

            throw new CustomError(404, $"User not found!");
        }

        public async Task<ForgotPassEntity> ValidateToken(string token)
        {

            var userForgotPass = await _ctx.ForgotPassEntity.FirstOrDefaultAsync(u => u.Token.ToLower() == token.ToLower());

            if (userForgotPass != null)
            {
                return userForgotPass;
            }

            throw new CustomError(404, $"Token not found!");
        }

        public async Task<string> UpdateForgotPassTable(User user)
        {
            var newToken = CreateRandomToken();

            var updateTable = new ForgotPassEntity 
            {
                UserId= user.UserId,
                Token=newToken,
                Expire=DateTime.Now.AddHours(1),
            };

            _ctx.ForgotPassEntity.Add(updateTable);

            await _ctx.SaveChangesAsync();

            return newToken;

        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
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

        public async Task<bool> DeleteFromTable(int id)
        {
            var result = await _ctx.ForgotPassEntity.FirstOrDefaultAsync(x=>x.ForgotPassEntityId== id);

            _ctx.Remove(result);

            await _ctx.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateNewPassword(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;

            await _ctx.SaveChangesAsync();

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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
