using tajmautAPI.Interfaces;
using tajmautAPI.Models;

namespace tajmautAPI.Repositories
{
    public class HelperValidationClassRepository : IHelperValidationClassRepository
    {

        private readonly tajmautDataContext _ctx;

        public HelperValidationClassRepository(tajmautDataContext ctx)
        {
            _ctx = ctx;
        }

        //check duplicates
        public async Task<User> CheckDuplicatesEmail(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        //check duplicates without the current user
        public async Task<User> CheckDuplicatesEmailWithId(string email, int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserId != id);
        }
    }
}
