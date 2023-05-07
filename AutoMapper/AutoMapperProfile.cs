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

            CreateMap<OnlineReservation, ReservationRESPONSE>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name));


            CreateMap<Comment, CommentRESPONSE>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName)); 

            CreateMap<CategoryEvent, CategoryRESPONSE>();

            CreateMap<Venue_City, Venue_CityRESPONSE>();

            CreateMap<Venue_Types, VenueTypeRESPONSE>();

            CreateMap<Venue, VenueRESPONSE>()
                .ForMember(dest => dest.VenueType, opt => opt.MapFrom(src => src.VenueType))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new Location { lat = src.lat, lng = src.lng }))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => new GalleryImages 
                { 
                    GalleryImage1= src.GalleryImage1,
                    GalleryImage2= src.GalleryImage2,
                    GalleryImage3= src.GalleryImage3,
                    GalleryImage4= src.GalleryImage4,
                    GalleryImage5= src.GalleryImage5,
                }))
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

