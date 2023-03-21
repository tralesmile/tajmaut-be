using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Repositories.Interfaces;

namespace tajmautAPI.Repositories.Implementations
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
