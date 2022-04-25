using GamesApi.Models;
using GamesApi.Models.Dtos;

namespace GamesApi.Services.IServices;

public interface ISystemRequirementsServices
{
    Task<Response<SystemRequirement>> GetSystemRequirementForGame(string gameName);
    Task<Response<bool>> AddSystemRequirementForGame(AddSystemRequirementDto dto);
    Task<Response<SystemRequirement>> UpdateSystemRequirementForGame(string gameName, UpdateSystemRequirementDto dto);
    Task<Response<bool>> DeleteSystemRequirementForGame(string gameName);
}