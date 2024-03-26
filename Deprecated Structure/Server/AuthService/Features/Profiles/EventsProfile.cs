using AuthService.Infrastructure.Data.Event.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AutoMapper;

namespace AuthService.Features.Profiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            // source --> target
            CreateMap<LoginPublishedDto, UserLoginDto>();
            CreateMap<RegisterPublishedDto, UserRegisterDto>();
        }
    }
}