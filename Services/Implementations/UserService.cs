using AutoMapper;
using Microsoft.Identity.Client;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IHelperValidationClassService _helperClass;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IHelperValidationClassService helperClass, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _helperClass = helperClass;
        }

        public async Task<ServiceResponse<UserRESPONSE>> CreateUserAsync(UserPostREQUEST user)
        {

            ServiceResponse<UserRESPONSE> result = new();

            try
            {
                //get user from repo
                var getUser = await _repo.CreateUserAsync(user);

                //check for duplicates with a method that saves data
                var checkUser = await _helperClass.CheckDuplicatesEmail(getUser.Email);

                //check email and phone Regex
                if (_helperClass.ValidateEmailRegex(getUser.Email))
                {
                    //checking for duplicates
                    if (checkUser == null)
                    {
                        if (user.Password == user.ConfirmPassword)
                        {
                            var resultSend = await _repo.AddEntity(getUser);
                            result.Data = _mapper.Map<UserRESPONSE>(resultSend);
                        }
                        else
                        {
                            throw new CustomError(400, $"Passwords do not match!");
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;

        }

        public async Task<ServiceResponse<UserRESPONSE>> DeleteUserAsync(int id)
        {

            ServiceResponse<UserRESPONSE> result = new();

            try
            {
                if (_helperClass.ValidateId(id))
                {
                    var currentUserID = _helperClass.GetMe();
                    var currentUserRole = _helperClass.GetCurrentUserRole();

                    //current user can make changes and Admins
                    if (id == currentUserID || _helperClass.CheckUserAdmin())
                    {
                        //get result from repo
                        var user = await _repo.DeleteUserAsync(id);

                        //check if there is any
                        if (user != null)
                        {
                            var resultSend = await _repo.DeleteEntity(user);
                            result.Data = _mapper.Map<UserRESPONSE>(resultSend);
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<List<UserRESPONSE>>> GetAllUsersAsync()
        {

            ServiceResponse<List<UserRESPONSE>> result = new();

            try
            {
                var resultSend = await _repo.GetAllUsersAsync();
                if (resultSend != null)
                {
                    result.Data = _mapper.Map<List<UserRESPONSE>>(resultSend);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public int GetMe()
        {
            return _helperClass.GetMe();
        }

        public async Task<ServiceResponse<UserRESPONSE>> GetUserByIdAsync(int id)
        {

            ServiceResponse<UserRESPONSE> result = new();

            try
            {
                //if id is smaller than 0
                if (_helperClass.ValidateId(id))
                {


                    var currentUserID = _helperClass.GetMe();
                    //current user can make changes and Admins
                    if (id == currentUserID || _helperClass.CheckUserAdmin())
                    {

                        //get result
                        var resultSend = await _repo.GetUserByIdAsync(id);

                        //if is true
                        if (resultSend != null)
                        {
                            result.Data = _mapper.Map<UserRESPONSE>(resultSend);
                        }
                    }

                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<UserRESPONSE>> UpdateUserAsync(UserPutREQUEST request, int id)
        {

            ServiceResponse<UserRESPONSE> result = new();

            try
            {
                if (id < 0)
                {
                    throw new CustomError(400, $"Invalid ID");
                }
                var currentUserID = _helperClass.GetMe();
                //current user can make changes and Admins
                if (id == currentUserID || _helperClass.CheckUserAdmin())
                {
                    //get result from repo
                    var getUser = await _repo.UpdateUserAsync(request, id);

                    //check if there is any
                    if (getUser != null)
                    {
                        //check for duplicates
                        var checkUser = await _helperClass.CheckDuplicatesEmailWithId(request.Email, getUser.UserId);

                        //check email and phone
                        if (_helperClass.ValidateEmailRegex(request.Email))
                        {
                            //checking for duplicates
                            if (checkUser == null)
                            {
                                //update the user 
                                var resultSend = await _repo.SaveChanges(getUser, request);
                                result.Data = _mapper.Map<UserRESPONSE>(resultSend);
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<UserRESPONSE>> UpdateUserPassword(UserPassREQUEST request, int id)
        {

            ServiceResponse<UserRESPONSE> result = new();

            try
            {
                var currentUserID = _helperClass.GetMe();
                //check if admin or the current user
                if (currentUserID == id || _helperClass.CheckUserAdmin())
                {
                    //check if user exists
                    if (await _helperClass.CheckIdUser(id))
                    {
                        //check old password
                        if (await _repo.CheckOldPassword(request.OldPassword, id))
                        {
                            var currentUser = await _repo.GetUserByIdAsync(id);
                            //new == confirm pass
                            if (request.NewPassword == request.ConfirmPassword)
                            {
                                var resultUser = await _repo.UpdatePassword(currentUser, request.NewPassword);
                                result.Data = _mapper.Map<UserRESPONSE>(currentUser);
                            }
                            else
                            {
                                throw new CustomError(400, $"Passwords do not match!");
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }
    }
}
