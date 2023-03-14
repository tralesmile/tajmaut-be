using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public CommentRepository(tajmautDataContext ctx,IHelperValidationClassService helper)
        {
            _ctx= ctx;
            _helper= helper;
        }

        public async Task<Comment> AddToDB(CommentREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var newComment = new Comment 
            {
                RestaurantId = request.RestaurantId,
                UserId= currentUserID,
                Body= request.Body,
                Review = request.Review,
            };

            _ctx.Comments.Add(newComment);
            await _ctx.SaveChangesAsync();

            return newComment;

        }
    }
}
