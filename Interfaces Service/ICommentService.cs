using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Interfaces_Service
{
    public interface ICommentService
    {
        Task<CommentRESPONSE> CreateComment(CommentREQUEST request);
    }
}
