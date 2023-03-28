using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        [HttpPut("ChangeUserRole")]
        public async Task<ActionResult> ChangeUserRole(UserRoleREQUEST request)
        {
            return Ok(request);
        }

    }
}
