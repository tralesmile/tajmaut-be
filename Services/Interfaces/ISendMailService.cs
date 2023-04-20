using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Services.Implementations;

namespace TajmautMK.Core.Services.Interfaces
{
    /// <summary>
    /// Represents a service for sending emails related to password reset and recovery.
    /// </summary>
    public interface ISendMailService
    {
        /// <summary>
        /// Sends an email with instructions on resetting the password for the user with the given email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A <see cref="ServiceResponse{T}"/> containing information about the operation.</returns>
        Task<ServiceResponse<string>> ForgotPassword(string email);

        /// <summary>
        /// Updates the password for the user associated with the given password reset token.
        /// </summary>
        /// <param name="request">The password reset request containing the new password.</param>
        /// <returns>A <see cref="ServiceResponse{T}"/> containing information about the operation.</returns>
        Task<ServiceResponse<ForgotPassEntity>> UpdateForgotPassword(ResetPasswordREQUEST request);
    }
}
