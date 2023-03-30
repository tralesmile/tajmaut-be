using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
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
