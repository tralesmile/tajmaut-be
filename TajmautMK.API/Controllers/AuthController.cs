using Microsoft.AspNetCore.Mvc;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Core.Services.Interfaces;

namespace TajmautMK.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _login;

        /// <summary>
        /// Constructor that initializes the controller with an instance of ILoginService.
        /// </summary>
        /// <param name="iservices">An instance of ILoginService.</param>
        public AuthController(ILoginService iservices)
        {
            _login = iservices;
        }

        /// <summary>
        /// Endpoint for user login.
        /// </summary>
        /// <param name="request">A LoginREQUEST object that contains user login information.</param>
        /// <returns>An ActionResult containing a TokenRESPONSE object if the login is successful, or an UnauthorizedResult with an error message if it fails.</returns>
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
