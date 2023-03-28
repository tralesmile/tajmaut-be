using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Services.Implementations
{
    public class LoginService : ILoginService
    {

        private readonly IConfiguration _configuration;

        private readonly ILoginRepository _repo;

        public LoginService(IConfiguration configuration, ILoginRepository repo)
        {
            _configuration = configuration;

            _repo = repo;
        }

        //User login method
        public async Task<string> Login(LoginREQUEST request)
        {
            //get user from repo
            var user = await _repo.Login(request.Email, request.Password);

            //if email is wrong or password is wrong
            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            //create token
            string token = CreateToken(user);

            return token;
        }

        //creating token with secret key
        public string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim("First Name",user.FirstName),
                new Claim("Last Name",user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //verifying hashed password
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
