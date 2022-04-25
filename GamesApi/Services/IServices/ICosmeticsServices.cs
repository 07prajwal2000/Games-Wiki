using GamesApi.Models;
using GamesApi.Models.Dtos;

namespace GamesApi.Services.IServices;

public interface ICosmeticsServices
{
    Task<Response<List<Cosmetic>>> GetCosmetics(int limit = 10, int skip = 0);
    Task<Response<Cosmetic>> GetCosmeticById(int id);
    Task<Response<Cosmetic>> GetCosmeticByName(string cosmeticName);
    Task<Response<bool>> AddCosmetic(AddCosmeticDto dto);
    Task<Response<Cosmetic>> UpdateCosmetic(string cosmeticName, UpdateCosmeticDto dto);
    Task<Response<int>> DeleteCosmetic(int id);
}