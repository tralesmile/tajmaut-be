using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces_Service
{
    public interface ILoginService
    {
        string CreateToken(User user);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<string> Login(LoginREQUEST request);
    }
}
