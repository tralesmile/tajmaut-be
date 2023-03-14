using System.Net;
using tajmautAPI.Exceptions;
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
                DateTime = DateTime.Now,
                CreatedAt= DateTime.Now,
                CreatedBy= currentUserID,
                ModifiedAt = DateTime.Now,
                ModifiedBy= currentUserID,
            };

            _ctx.Comments.Add(newComment);
            await _ctx.SaveChangesAsync();

            return newComment;

        }

        public async Task<List<Comment>> GetAllComments()
        {
            var allComments = await _ctx.Comments.ToListAsync();

            if (allComments.Count() > 0)
                return allComments;

            throw new CustomException(HttpStatusCode.NotFound, $"No comments found");
        }
    }
}
