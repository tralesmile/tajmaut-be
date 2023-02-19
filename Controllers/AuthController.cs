﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;

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
        public async Task<ActionResult<string>> Login(string email,string password)
        {
            //get token from services
            var token = await _login.Login(email, password);

            if(token == null)
            {
                return Unauthorized("Wrong email or password!");
            }

            return token;

        }
        
        
    }
}
