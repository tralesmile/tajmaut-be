using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Data;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class AdminRepository : IAdminRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public AdminRepository(tajmautDataContext ctx, IHelperValidationClassService helper)
        {
            _ctx = ctx;
            _helper = helper;
        }

        public async Task<User> UpdateUserAsync(User user, UserRoleREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            user.Role = request.Role;
            user.ModifiedAt = DateTime.Now;
            user.ModifiedBy = currentUserID;

            await _ctx.SaveChangesAsync();

            return user;
        }
    }
}
