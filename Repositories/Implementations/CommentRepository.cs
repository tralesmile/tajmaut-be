using System.Net;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public CommentRepository(tajmautDataContext ctx, IHelperValidationClassService helper)
        {
            _ctx = ctx;
            _helper = helper;
        }

        //add comment to db
        public async Task<Comment> AddToDB(CommentREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var newComment = new Comment
            {
                RestaurantId = request.RestaurantId,
                UserId = currentUserID,
                Body = request.Body,
                Review = request.Review,
                DateTime = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserID,
                ModifiedAt = DateTime.Now,
                ModifiedBy = currentUserID,
            };

            _ctx.Comments.Add(newComment);
            await _ctx.SaveChangesAsync();

            return newComment;

        }

        //delete comment
        public async Task<bool> DeleteComment(Comment comment)
        {
            _ctx.Comments.Remove(comment);
            await _ctx.SaveChangesAsync();
            return true;
        }

        //get all comments
        public async Task<List<Comment>> GetAllComments()
        {
            var allComments = await _ctx.Comments.ToListAsync();

            if (allComments.Count() > 0)
                return allComments;

            throw new CustomError(404, $"No comments found");
        }

        //update comment
        public async Task<Comment> UpdateComment(Comment comment, CommentREQUEST request)
        {
            var currentUserID = _helper.GetMe();

            comment.UserId = currentUserID;
            comment.RestaurantId = request.RestaurantId;
            comment.Review = request.Review;
            comment.Body = request.Body;
            comment.ModifiedAt = DateTime.Now;
            comment.ModifiedBy = currentUserID;

            await _ctx.SaveChangesAsync();

            return comment;
        }
    }
}
