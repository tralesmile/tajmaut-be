using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Repository for administrative operations.
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// Updates the user's role.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="request">The user role request data.</param>
        /// <returns>The updated user.</returns>
        Task<User> UpdateUserAsync(User user, UserRoleREQUEST request);
    }
}
