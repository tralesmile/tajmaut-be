using tajmautAPI.Models;

namespace tajmautAPI.Interfaces_Service
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserPOST request);
        Task<User> UpdateUserAsync(UserPOST request, int id);
        Task<User> DeleteUserAsync(int id);

    }
}
