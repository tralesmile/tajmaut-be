using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;

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

            var result = await _service.CreateComment(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        //delete
        [HttpDelete("DeleteComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            var result = await _service.DeleteComment(commentId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Deleted");

        }

        //update
        [HttpPut("UpdateComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> UpdateComment(CommentREQUEST request,int commentId)
        {
            var result = await _service.UpdateComment(request, commentId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        //get comment by restaurant
        [HttpGet("GetCommentsByRestaurantID"), AllowAnonymous]
        public async Task<ActionResult> GetCommentsByRestaurantID(int restaurantId)
        {
            var result = await _service.GetCommentsByRestaurantID(restaurantId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);

        }
    }
}
