using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.EntityClasses;

namespace tajmautAPI.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> Login(string email, string password);
    }
}
