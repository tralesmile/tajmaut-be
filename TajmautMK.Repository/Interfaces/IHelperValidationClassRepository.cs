using tajmautAPI.Models.EntityClasses;

namespace TajmautMK.Repository.Interfaces
{
    public interface IHelperValidationClassRepository
    {
        //check if there is user with same email
        Task<User> CheckDuplicatesEmail(string email);

        //check duplicates for updating
        Task<User> CheckDuplicatesEmailWithId(string email, int id);
        Task<bool> CheckIdVenue(int id);
        Task<bool> CheckIdCategory(int id);
        Task<bool> CheckIdEvent(int id);
        Task<bool> CheckIdEventActivity(int id);
        Task<bool> CheckIdEventDate(int id);
        Task<bool> CheckIdUser(int id);
        Task<bool> CheckIdComment(int id);
        Task<bool> CheckEventVenueRelation(int venueId, int eventId);
        Task<Comment> GetCommentId(int id);
        Task<OnlineReservation> CheckIdReservation(int id);
        Task<User> GetUserWithEmail(string email);
        Task<bool> CheckVenueTypeId(int id);

    }
}
