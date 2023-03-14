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
                if(await _helper.CheckIdRestaurant(request.RestaurantId))
                {
                    var result = await _repo.AddToDB(request);
                    return _mapper.Map<CommentRESPONSE>(result);
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
