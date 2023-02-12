using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using tajmautAPI.Interfaces;
using tajmautAPI.Models;
using tajmautAPI.Repositories;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        public UsersController(IUserRepository users)
        {
            _users=users;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _users.GetAllUsersAsync();
            if(users != null)
                return Ok(users);
            else
                return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _users.GetUserByIdAsync(id);
            if(user != null)            
                return Ok(user);
            else
                return NotFound();

        }
        [HttpPost]
        public async Task<ActionResult> Create(UserPOST user)
        {
            return Ok(await _users.CreateUserAsync(user));
        }
        [HttpPut]
        public async Task<ActionResult> Put(UserPOST request, int id)
        {
            var user = await _users.UpdateUserAsync(request, id);
            if(user != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _users.DeleteUserAsync(id);
            if(user != null )
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
