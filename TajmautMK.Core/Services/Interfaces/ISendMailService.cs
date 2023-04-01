using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.Core.Services.Interfaces
{
    public interface ISendMailService
    {
        Task<ServiceResponse<UserRESPONSE>> ForgotPassword(string email);
        Task<ServiceResponse<ForgotPassEntity>> UpdateForgotPassword(string token,ResetPasswordREQUEST request);
    }
}
