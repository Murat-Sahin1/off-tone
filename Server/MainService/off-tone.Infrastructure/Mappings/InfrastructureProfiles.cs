using AutoMapper;
using off_tone.Infrastructure.Dtos;
using off_tone.Application.Dtos.IdentityDtos.User;

namespace off_tone.Infrastructure.Mappings;

public class InfrastructureProfiles : Profile
{
    public InfrastructureProfiles()
    {
        // Source ---> Target
        CreateMap<UserLoginDto, PublishLoginEventDto>();
    }
}