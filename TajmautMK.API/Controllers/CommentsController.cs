using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Controllers
{
    /// <summary>
    /// Controller for managing comments on venues.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentService _service;

        /// <summary>
        /// Constructor for the CommentsController.
        /// </summary>
        /// <param name="commentService">The service responsible for handling comments.</param>
        public CommentsController(ICommentService commentService)
        {
            _service= commentService;
        }

        /// <summary>
        /// Endpoint for creating a new comment.
        /// </summary>
        /// <param name="request">The comment request data.</param>
        /// <returns>The newly created comment.</returns>
        [HttpPost("CreateComment"),Authorize(Roles ="Admin,Manager,User")]
        public async Task<ActionResult> CreateComment(CommentREQUEST request)
        {

            var result = await _service.CreateComment(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);


        }

        /// <summary>
        /// Endpoint for deleting a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A confirmation message.</returns>
        [HttpDelete("DeleteComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            var result = await _service.DeleteComment(commentId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Deleted");

        }

        /// <summary>
        /// Endpoint for updating a comment.
        /// </summary>
        /// <param name="request">The updated comment data.</param>
        /// <param name="commentId">The ID of the comment to update.</param>
        /// <returns>The updated comment.</returns>
        [HttpPut("UpdateComment"), Authorize(Roles = "Admin,Manager,User")]
        public async Task<ActionResult> UpdateComment(CommentREQUEST request,int commentId)
        {
            var result = await _service.UpdateComment(request, commentId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }

        /// <summary>
        /// Endpoint for getting all comments for a specific venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to retrieve comments for.</param>
        /// <returns>A list of comments for the specified venue.</returns>
        [HttpGet("GetCommentsByVenueID"), AllowAnonymous]
        public async Task<ActionResult> GetCommentsByVenueID(int venueId)
        {
            var result = await _service.GetCommentsByVenueID(venueId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);

        }
    }
}
