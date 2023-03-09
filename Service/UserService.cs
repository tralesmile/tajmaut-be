using AutoMapper;
using Microsoft.Identity.Client;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Service
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

        public async Task<UserRESPONSE> CreateUserAsync(UserPostREQUEST user)
        {
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
                            var result = await _repo.AddEntity(getUser);
                            return _mapper.Map<UserRESPONSE>(result);
                        }
                        else
                        {
                            throw new CustomException(HttpStatusCode.BadRequest,$"Passwords do not match!");
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");

        }

        public async Task<UserRESPONSE> DeleteUserAsync(int id)
        {
            try
            {
                if (_helperClass.ValidateId(id))
                {
                    var currentUserID = _helperClass.GetMe();
                    var currentUserRole = _helperClass.GetCurrentUserRole();

                    //current user can make changes and Admins
                    if ((id == currentUserID) || _helperClass.CheckUserAdmin())
                    {
                        //get result from repo
                        var user = await _repo.DeleteUserAsync(id);

                        //check if there is any
                        if (user != null)
                        {
                            var result = await _repo.DeleteEntity(user);
                            return _mapper.Map<UserRESPONSE>(result);
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        public async Task<List<UserRESPONSE>> GetAllUsersAsync()
        {
            try
            {
                var result = await _repo.GetAllUsersAsync();
                if (result != null)
                {
                    return _mapper.Map<List<UserRESPONSE>>(result);
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        public int GetMe()
        {
            return _helperClass.GetMe();
        }

        public async Task<UserRESPONSE> GetUserByIdAsync(int id)
        {
            try
            {
                //if id is smaller than 0
                if (_helperClass.ValidateId(id))
                {


                    var currentUserID = _helperClass.GetMe();
                    //current user can make changes and Admins
                    if ((id == currentUserID) || _helperClass.CheckUserAdmin())
                    {

                        //get result
                        var result = await _repo.GetUserByIdAsync(id);

                        //if is true
                        if (result != null)
                        {
                            return _mapper.Map<UserRESPONSE>(result);
                        }
                    }

                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        public async Task<UserRESPONSE> UpdateUserAsync(UserPutREQUEST request, int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new CustomException(HttpStatusCode.BadRequest,$"Invalid ID");
                }
                var currentUserID = _helperClass.GetMe();
                //current user can make changes and Admins
                if ((id == currentUserID) || _helperClass.CheckUserAdmin())
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
                                var result = await _repo.SaveChanges(getUser, request);
                                return _mapper.Map<UserRESPONSE>(result);
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        public async Task<UserRESPONSE> UpdateUserPassword(UserPassREQUEST request, int id)
        {
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
                                return _mapper.Map<UserRESPONSE>(currentUser);
                            }
                            else
                            {
                                throw new CustomException(HttpStatusCode.BadRequest,$"Passwords do not match!");
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }
    }
}
