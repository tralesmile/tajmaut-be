﻿using AutoMapper;
using System.Net;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace tajmautAPI.Services.Implementations
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
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;
        }

        //delete comment
        public async Task<ServiceResponse<CommentRESPONSE>> DeleteComment(int commentId)
        {

            ServiceResponse<CommentRESPONSE> result = new();

            try
            {
                //if comment exists
                if (await _helper.CheckIdComment(commentId))
                {

                    var currentUserID = _helper.GetMe();

                    //get comment obj
                    var getComment = await _helper.GetCommentId(commentId);

                    if (getComment != null)
                    {
                        //only comment user id or admin or manager can delete comment
                        if (currentUserID == getComment.UserId || _helper.CheckUserAdminOrManager())
                        {
                            if (await _repo.DeleteComment(getComment))
                            {
                                result.Data = _mapper.Map<CommentRESPONSE>(getComment);
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
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
                        var allComments = await _repo.GetAllComments();

                        if (allComments.Count() > 0)
                        {
                            //query for restaurant comments
                            var venueComments = allComments.Where(x => x.VenueId == venueId).ToList();

                            if (venueComments.Count() > 0)
                            {
                                //sord comments by date - newest first
                                var sortedVenueComments = venueComments.OrderByDescending(x => x.DateTime).ToList();

                                result.Data = _mapper.Map<List<CommentRESPONSE>>(sortedVenueComments);
                            }
                            else
                            {
                                throw new CustomError(404, $"This venue has no comments");
                            }
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
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
                        if (currentUserID == getComment.UserId)
                        {
                            var resultSend = await _repo.UpdateComment(getComment, request);
                            result.Data = _mapper.Map<CommentRESPONSE>(resultSend);
                        }
                        else
                        {
                            throw new CustomError(401, $"Unauthorized user access");
                        }
                    }
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.statusCode = ex.StatusCode;
                result.errorMessage = ex.ErrorMessage;
            }

            return result;

        }
    }
}
