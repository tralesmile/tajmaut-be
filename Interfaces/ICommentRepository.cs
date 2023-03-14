using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> AddToDB(CommentREQUEST request);
        Task<List<Comment>> GetAllComments();
        Task<bool> DeleteComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment,CommentREQUEST request);
    }
}
