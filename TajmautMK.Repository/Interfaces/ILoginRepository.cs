using tajmautAPI.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> Login(string email, string password);
    }
}
