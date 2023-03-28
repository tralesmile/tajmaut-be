using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Data;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Services.Interfaces;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Repositories.Interfaces;

namespace TajmautMK.Repository.Repositories.Implementations
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
            user.Role=request.Role;
            user.ModifiedAt=DateTime.Now;
            user.ModifiedBy=currentUserID;

            await _ctx.SaveChangesAsync();

            return user;
        }
    }
}
