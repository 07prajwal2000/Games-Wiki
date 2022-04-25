using AutoMapper;
using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;

namespace EFCoreLearning.Extras;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<AddGame, Game>();
        CreateMap<AddGameCharacterDto, GameCharacter>();
        CreateMap<AddSystemRequirementDto, SystemRequirement>();
    }
}