using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for the login service.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Logs in a user with the given login credentials.
        /// </summary>
        /// <param name="request">The login credentials.</param>
        /// <returns>The JWT token for the authenticated user.</returns>
        Task<string> Login(LoginREQUEST request);
    }
}
