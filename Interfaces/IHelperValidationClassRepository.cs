﻿using tajmautAPI.Models;

namespace tajmautAPI.Interfaces
{
    public interface IHelperValidationClassRepository
    {
        //check if there is user with same email
        Task<User> CheckDuplicatesEmail(string email);

        //check duplicates for updating
        Task<User> CheckDuplicatesEmailWithId(string email, int id);
        Task<bool> CheckIdRestaurant(int id);
        Task<bool> CheckIdCategory(int id);
        Task<bool> CheckIdEvent(int id);
        Task<bool> CheckIdEventActivity(int id);
        Task<bool> CheckIdEventDate(int id);
        Task<bool> CheckIdUser(int id);

    }
}
