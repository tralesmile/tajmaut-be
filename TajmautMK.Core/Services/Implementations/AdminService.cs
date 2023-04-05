using AutoMapper;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Services.Interfaces;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Core.Services.Implementations
{
    public class AdminService : IAdminService
    {

        private readonly IHelperValidationClassService _helper;
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public AdminService(IHelperValidationClassService helper,IAdminRepository repo,IMapper mapper)
        {
            _helper = helper;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserRESPONSE>> ChangeUserRole(UserRoleREQUEST request)
        {
            ServiceResponse<UserRESPONSE> result = new();
            try
            {
                if(_helper.ValidateEmailRegex(request.Email))
                {
                    var getUser = await _helper.GetUserWithEmail(request.Email);
                    var updatedUser = await _repo.UpdateUserAsync(getUser, request);
                    
                    result.Data = _mapper.Map<UserRESPONSE>(updatedUser);
                }
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.ErrorMessage = ex.ErrorMessage;
                result.statusCode = ex.StatusCode;
            }

            return result;
        }
    }
}
