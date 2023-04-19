﻿using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using System.Security.Cryptography;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Repository.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Data;
using TajmautMK.Common.Middlewares.Exceptions;


namespace TajmautMK.Repository.Implementations
{
    public class SendMailRepository : ISendMailRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IConfiguration _config;

        public SendMailRepository(tajmautDataContext ctx,IConfiguration configuration)
        {
            _ctx = ctx;
            _config = configuration;
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
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
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

        public async Task<bool> DeleteFromTable(ForgotPassEntity token)
        {

            _ctx.Remove(token);

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

        public string ForgotPasswordTemplate(User user, string token)
        {
            var template = "<h1>Здраво " + user.FirstName + "</h1>"
                        + "<h2>Имаш барање за промена на лозинката!</h2>" +
                        "<p>Кликни на оваа адреса за да ја промениш лозинката: http://tajmaut.ddns.net:3000/reset-password/" + token +
                        "<br><br>Ако не си го направил/а ова барање, тогаш игнорирај ја оваа порака!<br><br>Поздрав ТајмаутМК. 😃</p>";

            return template;
        }

        public string MailSender(MailSendREQUEST requst)
        {
            var emailTest = new MimeMessage();
            emailTest.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            emailTest.To.Add(MailboxAddress.Parse(requst.To));
            emailTest.Subject = requst.Subject;
            emailTest.Body = new TextPart(TextFormat.Html)
            {
                Text = requst.Template,
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(emailTest);

            smtp.Disconnect(true);

            return "Success";
        }

        public async Task<bool> CheckActiveForgotPassRequest(int id)
        {
            var now = DateTime.Now;
            var check = await _ctx.ForgotPassEntity.FirstOrDefaultAsync(x=> x.UserId==id && x.Expire > now);
             
            if (check != null)
            {
                throw new CustomError(400, $"Email has been sent");
            }

            return true;
        }

        public string ConfirmReservationTemplate(OnlineReservation reservation)
        {
            var status = reservation.IsActive ? "Пpифатена" : "На чекање";

            var template = "<h1>Здраво " + reservation.User.FirstName + "</h1>"
                        + "<h2>Овој емаил содржи детали за твојата резервација на настанот " + reservation.Event.Name + " во " + reservation.Venue.Name + "!</h2>" +
                        "<p>Статус на резервацијата: " + status +
                        "<br>Е-пошта: " + reservation.Email +
                        "<br>Име на резервацијата: " + reservation.FirstName + " " + reservation.LastName + 
                        "<br>Телефон: " + reservation.Phone +
                        "<br>Број на гости: " + reservation.NumberGuests + 
                        "<br>Име на настан: " + reservation.Event.Name +
                        "<br><br>За било какви информации обратете се на: " + reservation.Venue.Email + " , " + reservation.Venue.Phone + 
                        "<br>Ако не си ја направил/а оваа резервација, тогаш игнорирај ја оваа порака!<br><br>Поздрав ТајмаутМК. 😃</p>";

            return template;
        }
    }
}
