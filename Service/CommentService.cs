using AutoMapper;
using System.Net;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Service
{
    public class CommentService : ICommentService
    {

        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helper;

        public CommentService(ICommentRepository repo, IMapper mapper, IHelperValidationClassService helper)
        {
            _repo = repo;
            _mapper = mapper;
            _helper = helper;
        }

        //create comment
        public async Task<CommentRESPONSE> CreateComment(CommentREQUEST request)
        {
            try
            {
                //if valid id
                if (_helper.ValidateId(request.RestaurantId))
                {
                    //if restaurant exists
                    if (await _helper.CheckIdRestaurant(request.RestaurantId))
                    {
                        var result = await _repo.AddToDB(request);
                        return _mapper.Map<CommentRESPONSE>(result);
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        //delete comment
        public async Task<bool> DeleteComment(int commentId)
        {
            try
            {
                //if comment exists
                if(await _helper.CheckIdComment(commentId))
                {

                    var currentUserID = _helper.GetMe();
                    //get comment obj
                    var getComment = await _helper.GetCommentId(commentId);

                    if(getComment!= null)
                    {
                        //only comment user id or admin or manager can delete comment
                        if(currentUserID==getComment.UserId || _helper.CheckUserAdminOrManager())
                        {
                            if(await _repo.DeleteComment(getComment))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        //get comments by restaurant
        public async Task<List<CommentRESPONSE>> GetCommentsByRestaurantID(int restaurantId)
        {
            try
            {
                //if valid restaurant id
                if (_helper.ValidateId(restaurantId))
                {
                    //if restarant exists
                    if (await _helper.CheckIdRestaurant(restaurantId))
                    {
                        //all comments
                        var allComments = await _repo.GetAllComments();
                        
                        if (allComments.Count() > 0)
                        {
                            //query for restaurant comments
                            var restaurantComments = allComments.Where(x => x.RestaurantId == restaurantId).ToList();

                            if (restaurantComments.Count() > 0)
                            {
                                //sord comments by date - newest first
                                var sortedResComments = restaurantComments.OrderByDescending(x => x.DateTime).ToList();

                                return _mapper.Map<List<CommentRESPONSE>>(sortedResComments);
                            }
                            else
                            {
                                throw new CustomException(HttpStatusCode.NotFound, $"This restaurant has no comments");
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");
        }

        //update comment
        public async Task<CommentRESPONSE> UpdateComment(CommentREQUEST request,int commentId)
        {
            try
            {
                //check if comment exists
                if(await _helper.CheckIdComment(commentId))
                {
                    //if restaurant exists
                    if(await _helper.CheckIdRestaurant(request.RestaurantId))
                    {

                        var currentUserID = _helper.GetMe();
                        //get comment obj
                        var getComment = await _helper.GetCommentId(commentId);
                        
                        //only comment user id can update
                        if(currentUserID==getComment.UserId)
                        {
                            var result = await _repo.UpdateComment(getComment, request);
                            return _mapper.Map<CommentRESPONSE>(result);
                        }
                        else
                        {
                            throw new CustomException(HttpStatusCode.Unauthorized, $"Unauthorized user access");
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                throw;
            }

            throw new CustomException(HttpStatusCode.InternalServerError, $"Server error");

        }
    }
}
