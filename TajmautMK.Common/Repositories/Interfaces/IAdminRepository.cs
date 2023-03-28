using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Common.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        //update user
        Task<User> UpdateUserAsync(User user,UserRoleREQUEST request);
    }
}
