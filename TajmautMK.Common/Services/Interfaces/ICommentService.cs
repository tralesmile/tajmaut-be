using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceResponse<CommentRESPONSE>> CreateComment(CommentREQUEST request);
        Task<ServiceResponse<List<CommentRESPONSE>>> GetCommentsByRestaurantID(int restaurantId);
        Task<ServiceResponse<CommentRESPONSE>> DeleteComment(int commentId);
        Task<ServiceResponse<CommentRESPONSE>> UpdateComment(CommentREQUEST request, int commentId);
    }
}
