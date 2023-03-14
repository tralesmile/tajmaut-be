using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentService _service;

        public CommentsController(ICommentService commentService)
        {
            _service= commentService;
        }

        //create comment endpoint
        [HttpPost("CreateComment"),Authorize(Roles ="Admin,Manager,User")]
        public async Task<ActionResult> CreateComment(CommentREQUEST request)
        {
            try
            {
                var result = await _service.CreateComment(request);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }

        }

        [HttpDelete("DeleteComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> DeleteComment()
        {
            return Ok();
        }

        [HttpPut("UpdateComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> UpdateComment()
        {
            return Ok();
        }

        [HttpGet("GetCommentsByRestaurantID"), AllowAnonymous]
        public async Task<ActionResult> GetCommentsByRestaurantID(int restaurantId)
        {
            try
            {
                var result = await _service.GetCommentsByRestaurantID(restaurantId);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
        }
    }
}
