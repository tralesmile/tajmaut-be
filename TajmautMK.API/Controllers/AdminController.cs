using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Services.Interfaces;

namespace TajmautMK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService= adminService;
        }

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
