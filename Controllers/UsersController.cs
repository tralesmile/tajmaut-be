using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using tajmautAPI.Models;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly tajmautDataContext _context;
        public UsersController(tajmautDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
                if(user !=null)
                    return Ok(await _context.Users.Include(p => p.Comments).Where(p => p.UserId == id).ToListAsync());
            return NotFound();

        }
        [HttpPost]
        public async Task<ActionResult<List<User>>> Create(UserPOST request)
        {
            var newUser = new User
            {
                Email= request.Email,
                Password= request.Password,
                FirstName= request.FirstName,
                LastName = request.LastName,
                Address= request.Address,
                Phone= request.Phone,
                City= request.City,
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();


            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            return NotFound("Not found");
        }
        [HttpPut]
        public async Task<ActionResult<List<User>>> Put(UserPOST request,int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = request.Email;
                user.Password = request.Password;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Address = request.Address;
                user.Phone = request.Phone;
                user.City = request.City;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }
    }
}
