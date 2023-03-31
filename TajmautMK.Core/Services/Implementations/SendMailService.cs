﻿using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
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

        public SendMailService(ISendMailRepository repo,IMapper mapper,IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
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
                    emailTest.From.Add(MailboxAddress.Parse("wendy.ledner@ethereal.email"));
                    emailTest.To.Add(MailboxAddress.Parse("wendy.ledner@ethereal.email"));
                    emailTest.Subject = "Forgot Password Alert";
                    emailTest.Body = new TextPart(TextFormat.Html) { Text = "<h1>Need to reset your password?</h1><br><br><h2>Use your secret code!</h2><br>" +
                        token + "<h2><br><br>Click on the link below and enter the secret code .<br><br>[LINK]<br>" +
                        "If you did not forget your password, you can ignore this email.</h2>"};

                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("wendy.ledner@ethereal.email", "vZVkRGpJ2bEKq3yUsN");
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

        public async Task<ServiceResponse<ForgotPassEntity>> UpdateForgotPassword(ResetPasswordREQUEST request)
        {
            ServiceResponse<ForgotPassEntity> result = new();
            try
            {
                var checkToken = await _repo.ValidateToken(request.Token);
                if(checkToken.Expire < DateTime.Now) 
                {
                    await _repo.DeleteFromTable(checkToken.ForgotPassEntityId);
                    throw new CustomError(400, $"Token expired");
                }

                var user = await _repo.GetUserByIdAsync(checkToken.UserId);

                if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new CustomError(400, $"New password cant be the same as the old password");
                }

                await _repo.UpdateNewPassword(user,request.Password);
                await _repo.DeleteFromTable(checkToken.ForgotPassEntityId);

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
