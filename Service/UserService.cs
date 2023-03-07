using AutoMapper;
using Microsoft.Identity.Client;
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

        public UserService(IUserRepository repo,IHelperValidationClassService helperClass,IMapper mapper)
        {
            _mapper= mapper;
            _repo = repo;
            _helperClass = helperClass;
        }

        public async Task<UserRESPONSE> CreateUserAsync(UserPostREQUEST user)
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
                    var result = await _repo.AddEntity(getUser);
                    return _mapper.Map<UserRESPONSE>(result);
                }
                else
                {
                    throw new CustomBadRequestException($"User exists");
                }
            }
            else
            {
                throw new CustomBadRequestException($"Wrong email address!");
            }
            throw new CustomBadRequestException($"Invalid input");

        }

        public async Task<UserRESPONSE> DeleteUserAsync(int id)
        {

            if (id < 0)
                throw new CustomBadRequestException("Invalid ID");

            var currentUserID = _helperClass.GetMe();
            var currentUserRole = _helperClass.GetCurrentUserRole();

            //current user can make changes and Admins
            if ((id == currentUserID) || (currentUserRole == "Admin"))
            {
                //get result from repo
                var user = await _repo.DeleteUserAsync(id);

                //check if there is any
                if (user != null)
                {
                    var result = await _repo.DeleteEntity(user);
                    return _mapper.Map<UserRESPONSE>(result);
                }
                else
                {
                    throw new CustomNotFoundException($"User with ID {id} not found.");
                }
            }
            else
            {
                throw new CustomUnauthorizedException($"Unauthorized User!");
            }
            throw new CustomBadRequestException("Invalid input");
        }

        public async Task<List<UserRESPONSE>> GetAllUsersAsync()
        {
            var result = await _repo.GetAllUsersAsync();
            if(result!= null)
                return _mapper.Map<List<UserRESPONSE>>(result);
            throw new CustomNotFoundException("No data found!");
        }

        public int GetMe()
        {
            return _helperClass.GetMe();
        }

        public async Task<UserRESPONSE> GetUserByIdAsync(int id)
        {
            //if id is smaller than 0
            if(id < 0)
            {
                throw new CustomBadRequestException($"Invalid ID");
            }

            var currentUserID = _helperClass.GetMe();
            var currentUserRole = _helperClass.GetCurrentUserRole();
            //current user can make changes and Admins
            if ((id == currentUserID) || (currentUserRole == "Admin"))
            {

                //get result
                var result = await _repo.GetUserByIdAsync(id);

                //if is true
                if (result != null)
                {
                    return _mapper.Map<UserRESPONSE>(result);
                }
            }
            else
            {
                throw new CustomUnauthorizedException($"Unauthorized User!");
            }
            //if not found
            throw new CustomNotFoundException($"User with ID {id} not found.");
        }

        public async Task<UserRESPONSE> UpdateUserAsync(UserPostREQUEST request, int id)
        {
            if(id < 0)
            {
                throw new CustomBadRequestException($"Invalid ID");
            }
            var currentUserID = _helperClass.GetMe();
            var currentUserRole = _helperClass.GetCurrentUserRole();
            //current user can make changes and Admins
            if ((id == currentUserID) || (currentUserRole == "Admin"))
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
                        else
                        {
                            throw new CustomBadRequestException($"User exists!");
                        }
                    }
                    else
                    {
                        throw new CustomBadRequestException($"Invalid email!");
                    }
                }
                else
                {
                    throw new CustomNotFoundException($"User with ID {id} not found.");
                }
            }
            else
            {
                throw new CustomUnauthorizedException($"Unauthorized User!");
            }
                throw new CustomBadRequestException($"Invalid input");
        }

    }
}
