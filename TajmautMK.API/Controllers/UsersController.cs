using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Core.Services.Interfaces;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;

        public UsersController(IUserService userService,ISendMailService sendMailService)
        {
            _userService=userService;
            _sendMailService=sendMailService;
        }

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

        //Allow everyone to acces this endpoint
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

        [HttpGet("GetCurrentUserID"), Authorize(Roles ="Admin")]
        public ActionResult<int> GetCurrentUserEmail()
        {
            var userEmail = _userService.GetMe();
            return Ok(userEmail);
        }

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
            return Ok("Success");
        }

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
