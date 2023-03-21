using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {

        //get all users
        Task<List<User>> GetAllUsersAsync();

        //get user by id
        Task<User> GetUserByIdAsync(int id);

        //create user
        Task<User> CreateUserAsync(UserPostREQUEST request);

        //update user
        Task<User> UpdateUserAsync(UserPutREQUEST request, int id);

        //delete user
        Task<User> DeleteUserAsync(int id);

        //add new user to database
        Task<User> AddEntity(User user);

        //delete user from database
        Task<User> DeleteEntity(User user);

        //save the changes
        //request are the new changes
        Task<User> SaveChanges(User user, UserPutREQUEST request);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        //check old password
        Task<bool> CheckOldPassword(string oldPassword, int id);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        //update password
        Task<User> UpdatePassword(User user, string newPassword);



    }
}
