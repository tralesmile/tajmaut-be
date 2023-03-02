using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            if(users != null)
                return Ok(users);
            else
                return BadRequest();

        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> GetUserById(int id)
        {
            //get result from service
            var user = await _userService.GetUserByIdAsync(id);

            //check if there is any
            if(user != null)            
                return Ok(user);
            else
                return NotFound();

        }

        //Allow everyone to acces this endpoint
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Create(UserPostREQUEST user)
        {
            //get result from service
            var userCheck = await _userService.CreateUserAsync(user);

            //check if there is any
            if (userCheck != null)
            {
                return Ok(userCheck);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult> Put(UserPostREQUEST request, int id)
        {
            //get result from service
            var user = await _userService.UpdateUserAsync(request, id);

            //check if updated
            if(user != null)
            {
                return Ok("UPDATED");
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            //get result from service
            var user = await _userService.DeleteUserAsync(id);

            //check if there is any
            if(user != null )
            {
                return Ok("DELETED");
            }
            else
            {
                return NotFound("NOT FOUND");
            }
        }

        [HttpGet("GetCurrentUserID"), Authorize]
        public ActionResult<int> GetCurrentUserEmail()
        {
            var userEmail = _userService.GetMe();
            return Ok(userEmail);
        }

    }
}
