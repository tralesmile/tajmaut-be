using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.EntityClasses;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    public interface ISendMailRepository
    {
        /// <summary>
        /// Gets the user with the specified email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The User object with the specified email address, or null if not found.</returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// Updates the ForgotPass table with a new token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate a new token.</param>
        /// <returns>The new token generated for the user.</returns>
        Task<string> UpdateForgotPassTable(User user);

        /// <summary>
        /// Validates the specified token from the ForgotPass table.
        /// </summary>
        /// <param name="token">The token to validate.</param>
        /// <returns>The ForgotPassEntity object associated with the token, or null if the token is invalid or expired.</returns>
        Task<ForgotPassEntity> ValidateToken(string token);

        /// <summary>
        /// Gets the user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The User object with the specified ID, or null if not found.</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Deletes the specified token from the ForgotPass table.
        /// </summary>
        /// <param name="token">The token to delete.</param>
        /// <returns>True if the token was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteFromTable(ForgotPassEntity token);

        /// <summary>
        /// Updates the password for the specified user.
        /// </summary>
        /// <param name="user">The user whose password to update.</param>
        /// <param name="password">The new password for the user.</param>
        /// <returns>True if the password was updated successfully, false otherwise.</returns>
        Task<bool> UpdateNewPassword(User user, string password);

        /// <summary>
        /// Forgot password template for email sending.
        /// </summary>
        /// <param name="user">The user to send email.</param>
        /// <param name="token">The token for the user.</param>
        /// <returns>String with the body</returns>
        string ForgotPasswordTemplate(User user,string token);

        /// <summary>
        /// Forgot password mail sender.
        /// </summary>
        /// <param name="request">Object for mail send.</param>
        /// <returns>Messege 'Success' or fail.</returns>
        string MailSender(MailSendREQUEST request);

        /// <summary>
        /// Check if user has an active forgot pass request.
        /// </summary>
        /// <param name="id">The id of the user to check for active request for pass change.</param>
        /// <returns>Returns true if the user has not an active request othewise false.</returns>
        Task<bool> CheckActiveForgotPassRequest(int id);
    }
}
