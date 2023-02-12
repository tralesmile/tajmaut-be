using tajmautAPI.Models;

namespace tajmautAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserPOST user);
        Task<User> UpdateUserAsync(UserPOST request, int id);
        Task<User> DeleteUserAsync(int id);



    }
}
