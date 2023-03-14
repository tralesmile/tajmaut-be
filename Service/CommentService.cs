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

        public async Task<CommentRESPONSE> CreateComment(CommentREQUEST request)
        {
            try
            {
                if (_helper.ValidateId(request.RestaurantId))
                {
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

        public async Task<List<CommentRESPONSE>> GetCommentsByRestaurantID(int restaurantId)
        {
            try
            {
                if (_helper.ValidateId(restaurantId))
                {
                    if (await _helper.CheckIdRestaurant(restaurantId))
                    {
                        var allComments = await _repo.GetAllComments();
                        if (allComments.Count() > 0)
                        {
                            var restaurantComments = allComments.Where(x => x.RestaurantId == restaurantId).ToList();

                            if (restaurantComments.Count() > 0)
                            {
                                return _mapper.Map<List<CommentRESPONSE>>(restaurantComments);
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
    }
}
