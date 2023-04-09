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

        public async Task<ServiceResponse<string>> ForgotPassword(string email)
        {
            ServiceResponse<string> result = new();
            try
            {
                if (_helper.ValidateEmailRegex(email))
                {
                    //1.check if user exists
                    var user = await _repo.GetUserByEmail(email);

                    //2.update forgot password table & generate code
                    var token = await _repo.UpdateForgotPassTable(user);

                    //3.Send email
                    var template = _repo.ForgotPasswordTemplate(user, token);

                    var mailSend = new MailSendREQUEST 
                    {
                        Template = template,
                        To = email,
                        Subject = "Заборавена лозинка",
                    };

                    result.Data = _repo.MailSender(mailSend);
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
                //check for token
                var checkToken = await _repo.ValidateToken(request.Token);

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
