using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using tajmautAPI.Models.EntityClasses;
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
