using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Interfaces_Service
{
    public interface IUserService
    {
        Task<List<UserRESPONSE>> GetAllUsersAsync();
        Task<UserRESPONSE> GetUserByIdAsync(int id);
        Task<UserRESPONSE> CreateUserAsync(UserPostREQUEST request);
        Task<UserRESPONSE> UpdateUserAsync(UserPutREQUEST request, int id);
        Task<UserRESPONSE> DeleteUserAsync(int id);
        int GetMe();
        Task<UserRESPONSE> UpdateUserPassword(UserPassREQUEST request, int id);

    }
}
