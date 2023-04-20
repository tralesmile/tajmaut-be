using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    public interface IUserRepository
    {

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The data to create the new user.</param>
        /// <returns>The newly created user.</returns>
        Task<User> CreateUserAsync(UserPostREQUEST request);

        /// <summary>
        /// Updates a user with the specified ID.
        /// </summary>
        /// <param name="request">The updated data for the user.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns>The updated user.</returns>
        Task<User> UpdateUserAsync(UserPutREQUEST request, int id);

        /// <summary>
        /// Deletes a user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user.</returns>
        Task<User> DeleteUserAsync(int id);

        /// <summary>
        /// Adds a new user entity to the database.
        /// </summary>
        /// <param name="user">The user to add to the database.</param>
        /// <returns>The added user.</returns>
        Task<User> AddEntity(User user);

        /// <summary>
        /// Deletes a user entity from the database.
        /// </summary>
        /// <param name="user">The user to delete from the database.</param>
        /// <returns>The deleted user.</returns>
        Task<User> DeleteEntity(User user);

        /// <summary>
        /// Saves the changes made to a user entity.
        /// </summary>
        /// <param name="user">The user entity to save the changes for.</param>
        /// <param name="request">The updated data for the user.</param>
        /// <returns>The updated user.</returns>
        Task<User> SaveChanges(User user, UserPutREQUEST request);

        /// <summary>
        /// Creates a password hash and salt for the given password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The resulting password salt.</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        /// <summary>
        /// Checks if the given old password matches the user's current password.
        /// </summary>
        /// <param name="oldPassword">The old password to check.</param>
        /// <param name="id">The ID of the user to check the password for.</param>
        /// <returns>True if the old password matches, false otherwise.</returns>
        Task<bool> CheckOldPassword(string oldPassword, int id);

        /// <summary>
        /// Verifies if the provided password matches the stored password hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The password hash stored in the database.</param>
        /// <param name="passwordSalt">The password salt stored in the database.</param>
        /// <returns>True if the password matches the hash and salt, false otherwise.</returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        /// <summary>
        /// Updates the password of the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>The updated user object.</returns>
        Task<User> UpdatePassword(User user, string newPassword);




    }
}
