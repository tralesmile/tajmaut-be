using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Services.Interfaces;

namespace TajmautMK.API.Controllers
{
    /// <summary>
    /// Controller for performing administrative tasks, such as changing user roles.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        /// <summary>
        /// Constructor for initializing a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="adminService">The service used for performing administrative tasks.</param>
        public AdminController(IAdminService adminService)
        {
            _adminService= adminService;
        }

        /// <summary>
        /// Changes the role of a user.
        /// </summary>
        /// <param name="request">The request object containing the user ID and new role.</param>
        /// <returns>The updated user object.</returns>
        [HttpPut("ChangeUserRole")]
        public async Task<ActionResult> ChangeUserRole(UserRoleREQUEST request)
        {
            var result = await _adminService.ChangeUserRole(request);
            if(result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }
            return Ok(result.Data);
        }

    }
}
