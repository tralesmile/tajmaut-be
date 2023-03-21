using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Services.Interfaces
{
    public interface ILoginService
    {
        string CreateToken(User user);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<string> Login(LoginREQUEST request);
    }
}
