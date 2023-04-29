using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;

namespace TajmautMK.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods for managing user accounts.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>A service response containing the list of users.</returns>
        Task<ServiceResponse<List<UserRESPONSE>>> GetAllUsersAsync();

        /// <summary>
        /// Returns the user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A service response containing the retrieved user.</returns>
        Task<ServiceResponse<UserRESPONSE>> GetUserByIdAsync(int id);

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="request">A UserPostREQUEST object containing information about the new user.</param>
        /// <returns>A service response containing the created user account.</returns>
        Task<ServiceResponse<UserRESPONSE>> CreateUserAsync(UserPostREQUEST request);

        /// <summary>
        /// Updates an existing user account.
        /// </summary>
        /// <param name="request">A UserPutREQUEST object containing updated information for the user.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns>A service response containing the updated user account.</returns>
        Task<ServiceResponse<UserRESPONSE>> UpdateUserAsync(UserPutREQUEST request, int id);

        /// <summary>
        /// Deletes a user account.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A service response indicating whether the deletion was successful.</returns>
        Task<ServiceResponse<UserRESPONSE>> DeleteUserAsync(int id);

        /// <summary>
        /// Gets the ID of the currently authenticated user.
        /// </summary>
        /// <returns>The ID of the currently authenticated user.</returns>
        int GetMe();

        /// <summary>
        /// Updates the password for a user account.
        /// </summary>
        /// <param name="request">A UserPassREQUEST object containing the new password.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns>A service response indicating whether the password was updated successfully.</returns>
        Task<ServiceResponse<UserRESPONSE>> UpdateUserPassword(UserPassREQUEST request, int id);
    }
}
