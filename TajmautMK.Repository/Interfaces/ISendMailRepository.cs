using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.EntityClasses;
using TajmautMK.Common.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    public interface ISendMailRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<string> UpdateForgotPassTable(User user);
        Task<ForgotPassEntity> ValidateToken(string token);

        //get user by id
        Task<User> GetUserByIdAsync(int id);
        Task<bool> DeleteFromTable(ForgotPassEntity token);

        Task<bool> UpdateNewPassword(User user,string password);
    }
}
