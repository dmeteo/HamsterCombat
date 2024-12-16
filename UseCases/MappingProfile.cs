using AutoMapper;
using CSharpClicker.Domain;
using CSharpClicker.UseCases.GetBoosts;
using CSharpClicker.UseCases.GetCurrentUser;

namespace CSharpClicker.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
    }
}
