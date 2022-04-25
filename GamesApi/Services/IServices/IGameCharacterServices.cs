using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using LiteDB;

namespace EFCoreLearning.Services.IServices
{
    public interface IGameCharacterServices
    {
        Task<Response<List<GameCharacter>>> GetAllGameCharacters(int limit = 10, int skipCount = 0);
        Task<Response<GameCharacter?>> GetGameCharacterById(int id);
        Task<Response<List<GameCharacter>>> GetGameCharacterByGameName(string gameName);
        Task<Response<bool>> AddGameCharacterById(AddGameCharacterDto gameCharacterDto);
        Task<Response<GameCharacter?>> UpdateGameCharacterById(int id, AddGameCharacterDto updateGameCharacterDto);
        Task<Response<bool>> DeleteGameCharacterById(int id);
        Task<Response<List<string>>> GetAllGameCharacterNamesByGameName(string gameName, int limit = 10, int skipCount = 0);
    }
}
