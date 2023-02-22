using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models;

namespace tajmautAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> Login(string email, string password);
    }
}
