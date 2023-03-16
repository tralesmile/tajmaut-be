using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Service;

namespace tajmautAPI.Interfaces_Service
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserRESPONSE>>> GetAllUsersAsync();
        Task<ServiceResponse<UserRESPONSE>> GetUserByIdAsync(int id);
        Task<ServiceResponse<UserRESPONSE>> CreateUserAsync(UserPostREQUEST request);
        Task<ServiceResponse<UserRESPONSE>> UpdateUserAsync(UserPutREQUEST request, int id);
        Task<ServiceResponse<UserRESPONSE>> DeleteUserAsync(int id);
        int GetMe();
        Task<ServiceResponse<UserRESPONSE>> UpdateUserPassword(UserPassREQUEST request, int id);

    }
}
