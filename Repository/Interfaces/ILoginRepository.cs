using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Repository for handling login functionality.
    /// </summary>
    public interface ILoginRepository
    {
        /// <summary>
        /// Login a user with the given email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The User object if the login is successful, otherwise null.</returns>
        Task<User> Login(string email, string password);
    }
}
