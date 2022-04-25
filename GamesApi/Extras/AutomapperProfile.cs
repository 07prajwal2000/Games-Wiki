using AutoMapper;
using GamesApi.Models;
using GamesApi.Models.Dtos;

namespace GamesApi.Extras;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<AddGame, Game>();
        CreateMap<AddGameCharacterDto, GameCharacter>();
        CreateMap<AddSystemRequirementDto, SystemRequirement>();
    }
}