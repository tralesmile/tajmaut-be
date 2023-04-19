using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;

namespace TajmautMK.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods for changing the role of a user.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Changes the role of a user.
        /// </summary>
        /// <param name="request">The request containing the user ID and new role.</param>
        /// <returns>A ServiceResponse containing the updated user information.</returns>
        Task<ServiceResponse<UserRESPONSE>> ChangeUserRole(UserRoleREQUEST request);
    }
}
