using tajmautAPI.Models;

namespace tajmautAPI.Interfaces
{
    public interface IUserRepository
    {

        //get all users
        Task<List<User>> GetAllUsersAsync();

        //get user by id
        Task<User> GetUserByIdAsync(int id);

        //create user
        Task<User> CreateUserAsync(UserPOST request);

        //update user
        Task<User> UpdateUserAsync(UserPOST request, int id);

        //delete user
        Task<User> DeleteUserAsync(int id);

        //add new user to database
        Task<User> AddEntity(User user);

        //check if there is user with same email
        Task<User> CheckDuplicatesEmail(string email);

        //check duplicates for updating
        Task<User> CheckDuplicatesEmailWithId(string email,int id);

        //delete user from database
        Task<User> DeleteEntity (User user);

        //save the changes
        //request are the new changes
        Task<User> SaveChanges(User user, UserPOST request);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);


    }
}
