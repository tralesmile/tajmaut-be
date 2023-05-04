using AutoMapper;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Core.Services.Implementations
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
        public async Task<ServiceResponse<CommentRESPONSE>> CreateComment(CommentREQUEST request)
        {

            ServiceResponse<CommentRESPONSE> result = new();

            try
            {
                //if valid id
                if (_helper.ValidateId(request.VenueId))
                {
                    //if restaurant exists
                    if (await _helper.CheckIdVenue(request.VenueId))
                    {
                        var resultSend = await _repo.AddToDB(request);
                        result.Data = _mapper.Map<CommentRESPONSE>(resultSend);
                        result.ErrorMessage = "None";
                        result.statusCode = 201;//Created
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //delete comment
        public async Task<ServiceResponse<CommentRESPONSE>> DeleteComment(int commentId)
        {

            ServiceResponse<CommentRESPONSE> result = new();

            try
            {
                var currentUserID = _helper.GetMe();
                var commentByID = await _helper.GetCommentId(commentId);
                var venueID = commentByID.VenueId;

                //if comment exists
                if (await _helper.CheckIdComment(commentId))
                {
                    if (await _helper.CheckManagerVenueRelation(venueID, currentUserID))
                    {

                        //get comment obj
                        var getComment = await _helper.GetCommentId(commentId);

                        if (getComment != null)
                        {
                            //only comment user id or admin or manager can delete comment
                            if (_helper.CheckUserCommentRelation(getComment,currentUserID) || _helper.CheckUserAdminOrManager())
                            {
                                if (await _repo.DeleteComment(getComment))
                                {
                                    result.Data = _mapper.Map<CommentRESPONSE>(getComment);
                                }
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //get comments by venue
        public async Task<ServiceResponse<List<CommentRESPONSE>>> GetCommentsByVenueID(int venueId)
        {

            ServiceResponse<List<CommentRESPONSE>> result = new();

            try
            {
                //if valid restaurant id
                if (_helper.ValidateId(venueId))
                {
                    //if restarant exists
                    if (await _helper.CheckIdVenue(venueId))
                    {
                        //all comments
                        var allComments = await _repo.GetAllCommentsByVenue(venueId);

                        //sord comments by date - newest first
                        var sortedVenueComments = allComments.OrderByDescending(x => x.DateTime).ToList();

                        result.Data = _mapper.Map<List<CommentRESPONSE>>(sortedVenueComments);

                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //update comment
        public async Task<ServiceResponse<CommentRESPONSE>> UpdateComment(CommentREQUEST request, int commentId)
        {

            ServiceResponse<CommentRESPONSE> result = new();

            try
            {
                //check if comment exists
                if (await _helper.CheckIdComment(commentId))
                {
                    //if restaurant exists
                    if (await _helper.CheckIdVenue(request.VenueId))
                    {

                        var currentUserID = _helper.GetMe();
                        //get comment obj
                        var getComment = await _helper.GetCommentId(commentId);

                        //only comment user id can update
                        if (_helper.CheckUserCommentRelation(getComment,currentUserID))
                        {
                            var resultSend = await _repo.UpdateComment(getComment, request);
                            result.Data = _mapper.Map<CommentRESPONSE>(resultSend);
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.ErrorMessage = ex.ErrorMessage;
            }

            return result;

        }
    }
}
