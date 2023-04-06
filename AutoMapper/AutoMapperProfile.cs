using AutoMapper;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsRESPONSE;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsRESPONSE;

namespace tajmautAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRESPONSE>();

            CreateMap<Event, EventRESPONSE>();

            CreateMap<OnlineReservation, ReservationRESPONSE>();

            CreateMap<Comment, CommentRESPONSE>();

            CreateMap<CategoryEvent, CategoryRESPONSE>();

            CreateMap<Venue_City, Venue_CityRESPONSE>();

            CreateMap<Venue_Types, VenueTypeRESPONSE>();

            CreateMap<Venue, VenueRESPONSE>();

        }
    }
}
