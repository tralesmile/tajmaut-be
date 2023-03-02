using tajmautAPI.Models;

namespace tajmautAPI.Interfaces_Service
{
    public interface IHelperValidationClassService
    {
        //validate email regex
        bool ValidateEmailRegex(string emailRegex);

        //check if there is user with same email
        Task<User> CheckDuplicatesEmail(string email);

        //check duplicates for updating
        Task<User> CheckDuplicatesEmailWithId(string email, int id);
    }
}
