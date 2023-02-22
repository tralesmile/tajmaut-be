using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models;

namespace tajmautAPI.Interfaces_Service
{
    public interface ILoginService
    {
        string CreateToken(User user);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<string> Login(string email, string password);
    }
}
