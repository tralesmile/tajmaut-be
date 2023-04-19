using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Core.Services.Interfaces;

namespace TajmautMK.API.Controllers
{
    /// <summary>
    /// Controller for managing user accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;

        /// <summary>
        /// Constructor for the UsersController class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="sendMailService">The email service.</param>
        public UsersController(IUserService userService,ISendMailService sendMailService)
        {
            _userService=userService;
            _sendMailService=sendMailService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            //get result from service
            var result = await _userService.GetAllUsersAsync();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>Returns the user with the given ID.</returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> GetUserById(int id)
        {
            //get result from service
            var result = await _userService.GetUserByIdAsync(id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>Returns the created user.</returns>
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Create(UserPostREQUEST user)
        {
            //get result from service
            var result = await _userService.CreateUserAsync(user);

            //check if error exists
            if(result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
            
        }

        /// <summary>
        /// Update a user by ID.
        /// </summary>
        /// <param name="request">The updated user information.</param>
        /// <param name="id">The ID of the user to update.</param>
        /// <returns>Returns the updated user.</returns>
        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult> Put(UserPutREQUEST request, int id)
        {
            //get result from service
            var result = await _userService.UpdateUserAsync(request, id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns "Success" if the user is deleted successfully.</returns>
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult> Delete(int id)

        {
            //get result from service
            var result = await _userService.DeleteUserAsync(id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Success");
        }

        /// <summary>
        /// Get the ID of the current user.
        /// </summary>
        /// <returns>Returns the ID of the current user.</returns>
        [HttpGet("GetCurrentUserID"), Authorize(Roles ="Admin")]
        public ActionResult<int> GetCurrentUserEmail()
        {
            var userEmail = _userService.GetMe();
            return Ok(userEmail);
        }

        /// <summary>
        /// Update a user's password.
        /// </summary>
        /// <param name="request">The updated password information.</param>
        /// <param name="id">The ID of the user to update the password for.</param>
        /// <returns>Returns "Success" if the password is updated successfully.</returns>
        [HttpPut("UpdateUserPassword"),Authorize]
        public async Task<ActionResult> UpdateUserPassword(UserPassREQUEST request,int id)
        {
            var result = await _userService.UpdateUserPassword(request,id);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Success");
        }

        /// <summary>
        /// Send a password reset email to the user.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>Returns "Success" if the email is sent successfully.</returns>
        [HttpPost("ForgotPassword"), AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(string email)
        {

            var result = await _sendMailService.ForgotPassword(email);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Updates the password of the user with the provided reset password token.
        /// </summary>
        /// <param name="request">The new password,confirm and token.</param>
        /// <returns>An HTTP status code indicating the result of the operation.</returns>
        /// <remarks>This endpoint can be accessed anonymously.</remarks>
        [HttpPost("UpdateForgotPassword"), AllowAnonymous]
        public async Task<ActionResult> UpdateForgotPassword(ResetPasswordREQUEST request)
        {

            var result = await _sendMailService.UpdateForgotPassword(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Success");
        }

    }
}
