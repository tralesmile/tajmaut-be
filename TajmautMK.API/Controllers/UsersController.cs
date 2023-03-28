using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService=userService;
        }

        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            //get result from service
            var result = await _userService.GetAllUsersAsync();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> GetUserById(int id)
        {
            //get result from service
            var result = await _userService.GetUserByIdAsync(id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        //Allow everyone to acces this endpoint
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Create(UserPostREQUEST user)
        {
            //get result from service
            var result = await _userService.CreateUserAsync(user);

            //check if error exists
            if(result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
            
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult> Put(UserPutREQUEST request, int id)
        {
            //get result from service
            var result = await _userService.UpdateUserAsync(request, id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult> Delete(int id)

        {
            //get result from service
            var result = await _userService.DeleteUserAsync(id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Success");
        }

        [HttpGet("GetCurrentUserID"), Authorize(Roles ="Admin")]
        public ActionResult<int> GetCurrentUserEmail()
        {
            var userEmail = _userService.GetMe();
            return Ok(userEmail);
        }

        [HttpPut("UpdateUserPassword"),Authorize]
        public async Task<ActionResult> UpdateUserPassword(UserPassREQUEST request,int id)
        {
            var result = await _userService.UpdateUserPassword(request,id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Success");
        }

    }
}
