using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories;

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
            var users = await _userService.GetAllUsersAsync();

            //check if there is any
            try
            {
                if (users != null)
                    return Ok(users);
            }
            catch(Exception ex)
            {
                if(ex is CustomNotFoundException)
                {
                    return NotFound(ex.Message);
                }
            }

            return StatusCode(500);

        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> GetUserById(int id)
        {
            //get result from service
            var user = await _userService.GetUserByIdAsync(id);
            try
            {
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomBadRequestException) 
                    return BadRequest(ex.Message);
            }
            return null;

        }

        //Allow everyone to acces this endpoint
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Create(UserPostREQUEST user)
        {
            //get result from service
            var userCheck = await _userService.CreateUserAsync(user);

            try
            {
                //check if there is any
                if (userCheck != null)
                {
                    return Ok(userCheck);
                }
            }
            catch (Exception ex)
            {
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult> Put(UserPutREQUEST request, int id)
        {
            //get result from service
            var user = await _userService.UpdateUserAsync(request, id);

            try
            {
                //check if updated
                if (user != null)
                {
                    return Ok("UPDATED");
                }
            }
            catch (Exception ex)
            {
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if (ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);

            }

            return StatusCode(500);

        }

        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult> Delete(int id)

        {
            //get result from service
            var user = await _userService.DeleteUserAsync(id);

            try
            {
                //check if there is any
                if (user != null)
                {
                    return Ok("DELETED");
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if (ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpGet("GetCurrentUserID"), Authorize]
        public ActionResult<int> GetCurrentUserEmail()
        {
            var userEmail = _userService.GetMe();
            return Ok(userEmail);
        }

        [HttpPut("UpdateUserPassword"),Authorize]
        public async Task<ActionResult> UpdateUserPassword(UserPassREQUEST request,int id)
        {
            var result = await _userService.UpdateUserPassword(request,id);
            try
            {
                if(result != null)
                {
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                if(ex is CustomBadRequestException)
                    return BadRequest(ex.Message);
                if(ex is CustomNotFoundException)
                    return NotFound(ex.Message);
                if(ex is CustomUnauthorizedException)
                    return Unauthorized(ex.Message);
            }

            return StatusCode(500);
        }

    }
}
