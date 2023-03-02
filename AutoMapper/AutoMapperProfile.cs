using AutoMapper;
using tajmautAPI.Models;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.AutoMapper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRESPONSE>();
            CreateMap<Event, EventRESPONSE>();
        }
    }
}
