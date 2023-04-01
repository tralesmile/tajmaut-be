using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Core.Services.Implementations
{
    public class SendMailService : ISendMailService
    {

        private readonly ISendMailRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;
        private readonly IConfiguration _config;

        public SendMailService(ISendMailRepository repo,IMapper mapper,IHelperValidationClassService helper,IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
            _config = config;
        }

        public async Task<ServiceResponse<UserRESPONSE>> ForgotPassword(string email)
        {
            ServiceResponse<UserRESPONSE> result = new();
            try
            {
                if (_helper.ValidateEmailRegex(email))
                {
                    //1.check if user exists
                    var user = await _repo.GetUserByEmail(email);

                    //2.update forgot password table & generate code
                    var token = await _repo.UpdateForgotPassTable(user);

                    //3.Send email
                    var emailTest = new MimeMessage();
                    emailTest.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
                    emailTest.To.Add(MailboxAddress.Parse(email));
                    emailTest.Subject = "Forgot Password";
                    emailTest.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "<h1>Здраво " + user.FirstName + "</h1>"
                        + "<h2>Имаш барање за промена на лозинката!</h2>" +
                        "<br><p>Ова е твојот токен: " + token + " </p><br>" +
                        "<p>Кликни на оваа адреса за да ја промениш лозинката: https://tajmautmk.azurewebsites.net/api/Users/UpdateForgotPassword?token=" + token +
                        "<br><br>Ако не си го направил/а ова барање, тогаш игнорирај ја оваа порака!<br><br>Поздрав ТајмаутМК. 😃</p>"

                    };

                    using var smtp = new SmtpClient();
                    smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
                    smtp.Send(emailTest);

                    smtp.Disconnect(true);

                    result.Data = _mapper.Map<UserRESPONSE>(user);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<ForgotPassEntity>> UpdateForgotPassword(string token, ResetPasswordREQUEST request)
        {
            ServiceResponse<ForgotPassEntity> result = new();
            try
            {
                //check for token
                var checkToken = await _repo.ValidateToken(token);

                //check date
                if(checkToken.Expire < DateTime.Now) 
                {
                    //delete if expired
                    await _repo.DeleteFromTable(checkToken);
                    throw new CustomError(400, $"Token expired");
                }

                var user = await _repo.GetUserByIdAsync(checkToken.UserId);

                if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new CustomError(400, $"New password cant be the same as the old password");
                }

                await _repo.UpdateNewPassword(user,request.Password);

                await _repo.DeleteFromTable(checkToken);

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //verifying hashed password
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
