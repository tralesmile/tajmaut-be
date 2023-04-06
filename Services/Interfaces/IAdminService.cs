using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Common.Services.Interfaces
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
