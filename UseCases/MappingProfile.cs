using AutoMapper;
using CSharpClicker.Domain;
using CSharpClicker.UseCases.GetBoosts;

namespace CSharpClicker.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Boost, BoostDto>();
    }
}
