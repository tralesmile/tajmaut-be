﻿using tajmautAPI.Models;

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
        Task<bool> CheckIdRestaurant(int id);
        Task<bool> CheckIdCategory(int id);
        int GetMe();
        string GetCurrentUserEmail();
        string GetCurrentUserRole();
        Task<bool> CheckIdEvent(int id);
        Task<bool> CheckIdEventActivity(int id);
        Task<bool> CheckIdEventDate(int id);
        bool ValidatePhoneRegex(string phone);
        Task<bool> CheckIdUser(int id);
        bool CheckUserAdmin();
        bool CheckUserAdminOrManager();
        bool CheckUserManager();
        bool ValidateId(int id);
        Task<OnlineReservation> CheckIdReservation(int id);


    }
}
