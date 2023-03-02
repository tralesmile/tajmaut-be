using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces_Service
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserPostREQUEST request);
        Task<User> UpdateUserAsync(UserPostREQUEST request, int id);
        Task<User> DeleteUserAsync(int id);

    }
}
