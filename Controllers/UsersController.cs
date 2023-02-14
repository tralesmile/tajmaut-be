using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Repositories;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService=userService;
        }
        [HttpGet]
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
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<ActionResult> Create(UserPOST user)
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
                return BadRequest("Exists");
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(UserPOST request, int id)
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
                return NotFound("NOT FOUND");
            }

        }
        [HttpDelete]
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

    }
}
