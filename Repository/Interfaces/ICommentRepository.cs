using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Interface for accessing and manipulating comments in the database
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Adds a new comment to the database
        /// </summary>
        /// <param name="request">The request containing the comment data</param>
        /// <returns>The added comment</returns>
        Task<Comment> AddToDB(CommentREQUEST request);

        /// <summary>
        /// Retrieves all comments from the database
        /// </summary>
        /// <returns>A list of all comments</returns>
        Task<List<Comment>> GetAllComments();

        /// <summary>
        /// Retrieves all comments from the database with given venue id
        /// </summary>
        /// <param name="id">The id of the venue</param>
        /// <returns>A list of all comments on that venue</returns>
        Task<List<Comment>> GetAllCommentsByVenue(int id);

        /// <summary>
        /// Deletes a comment from the database
        /// </summary>
        /// <param name="comment">The comment to be deleted</param>
        /// <returns>A boolean indicating whether the deletion was successful</returns>
        Task<bool> DeleteComment(Comment comment);

        /// <summary>
        /// Updates an existing comment in the database
        /// </summary>
        /// <param name="comment">The comment to be updated</param>
        /// <param name="request">The request containing the updated comment data</param>
        /// <returns>The updated comment</returns>
        Task<Comment> UpdateComment(Comment comment, CommentREQUEST request);
    }
}
