using AutoMapper;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.AutoMapper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRESPONSE>();
            CreateMap<Event, EventRESPONSE>();
            CreateMap<OnlineReservation, ReservationRESPONSE>();
            CreateMap<Comment, CommentRESPONSE>();
            CreateMap<CategoryEvent, CategoryRESPONSE>();

        }
    }
}
