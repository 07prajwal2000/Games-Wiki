using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using LiteDB;

namespace EFCoreLearning.Services.IServices;

public interface IGamesServices
{
    Task<Response<IEnumerable<Game>>> GetGamesByLimit(int limit = 10, int skipCount = 0);
    Task<Response<IEnumerable<string>>> GetGameNamesByLimit(int limit = 10, int skipCount = 0);
    Task<Response<Game?>> GetGame(int id);
    Task<Response<bool>> AddGame(AddGame gameDto);
    Task<Response<Game?>> UpdateGame(int gameId, AddGame updateGameDto);
    Task<Response<bool>> DeleteGame(int gameId);
}