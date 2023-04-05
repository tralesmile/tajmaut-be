using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    /// <summary>
    /// Provides methods for creating, retrieving, updating, and deleting comments for venues.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Creates a new comment for a venue.
        /// </summary>
        /// <param name="request">The request object containing the comment details.</param>
        /// <returns>A response object containing the created comment.</returns>
        Task<ServiceResponse<CommentRESPONSE>> CreateComment(CommentREQUEST request);

        /// <summary>
        /// Retrieves all comments for a venue.
        /// </summary>
        /// <param name="venueId">The ID of the venue to retrieve comments for.</param>
        /// <returns>A response object containing a list of comments for the specified venue.</returns>
        Task<ServiceResponse<List<CommentRESPONSE>>> GetCommentsByVenueID(int venueId);

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A response object containing the deleted comment.</returns>
        Task<ServiceResponse<CommentRESPONSE>> DeleteComment(int commentId);

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="request">The request object containing the updated comment details.</param>
        /// <param name="commentId">The ID of the comment to update.</param>
        /// <returns>A response object containing the updated comment.</returns>
        Task<ServiceResponse<CommentRESPONSE>> UpdateComment(CommentREQUEST request, int commentId);
    }
}
