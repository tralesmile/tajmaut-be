using Microsoft.EntityFrameworkCore;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Data;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Repository.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly tajmautDataContext _ctx;

        public LoginRepository(tajmautDataContext ctx)
        {
            _ctx = ctx;
        }

        //login user
        public async Task<User> Login(string email, string password)
        {
            //get user from db
            var userCheck = await _ctx.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            return userCheck;
        }

    }
}
