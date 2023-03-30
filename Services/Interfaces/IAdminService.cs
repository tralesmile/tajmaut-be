using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Common.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResponse<UserRESPONSE>> ChangeUserRole(UserRoleREQUEST request);
    }
}
