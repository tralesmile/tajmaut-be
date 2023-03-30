using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _login;

        public AuthController(ILoginService iservices)
        {
            _login = iservices;
        }

        //user login endpoint
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginREQUEST request)
        {
            //get token from services
            var token = await _login.Login(request);

            if (token == null)
            {
                return Unauthorized("Wrong email or password!");
            }

            var responseToken = new TokenRESPONSE();
            responseToken.AccessToken = token;
            responseToken.Expires = DateTime.Now.AddHours(1);

            return Ok(responseToken);


        }
        
        
    }
}
