using AutoMapper;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.Enums;
using TajmautMK.Common.Models.ModelsRESPONSE;

namespace TajmautMK.API.AutoMapper
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

            CreateMap<Venue, VenueRESPONSE>()
                .ForMember(dest => dest.VenueType, opt => opt.MapFrom(src => src.VenueType))
                .ForMember(dest => dest.VenueCity, opt => opt.MapFrom(src => src.Venue_City));

            CreateMap<Event,EventGetRESPONSE>()
                .ForMember(dest => dest.VenueName, opt=> opt.MapFrom(src => src.Venue.Name))
                .ForMember(dest => dest.VenuePhone, opt => opt.MapFrom(src => src.Venue.Phone))
                .ForMember(dest => dest.VenueCity, opt => opt.MapFrom(src => src.Venue.Venue_City.CityName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryEvent.Name))
                .ForMember(dest => dest.StatusEvent, opt => opt.MapFrom(src =>
                    !src.isCanceled ?
                        src.DateTime > DateTime.Now ? EventStatus.Upcoming :
                        src.DateTime.AddHours((int)src.Duration) > DateTime.Now ? EventStatus.Ongoing :
                        EventStatus.Ended :
                        EventStatus.Canceled
                    ));

        }
    }
}
