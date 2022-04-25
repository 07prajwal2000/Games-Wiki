using AutoMapper;
using GamesApi.Data;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;

namespace GamesApi.Services;

public class CosmeticsServices : ICosmeticsServices
{
    private readonly LiteDbContext _dbContext;
    private readonly IMapper _mapper;

    public CosmeticsServices(LiteDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Response<List<Cosmetic>>> GetCosmetics(int limit = 10, int skip = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<List<Cosmetic>>();
            if (limit > 50)
            {
                response.Data = null;
                response.Message = "The Limit QueryParam has GreaterThan 50.";
                return response;
            }

            var data = _dbContext.Cosmetics.Query().Skip(skip).Limit(limit).ToList();
            response.Data = data;
            if (data.Count < 0)
            {
                response.Data = data;
                response.Message = "Not Found.";
                return response;
            }
            response.Message = "Found.";
            return response;
        });
    }
    
    public async Task<Response<Cosmetic>> GetCosmeticById(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<Cosmetic>();

            var data = _dbContext.Cosmetics.Query().Where(x => x.Id == id).FirstOrDefault();
            response.Data = data;
            if (data is null)
            {
                response.Message = "Not Found.";
                return response;
            }
            response.Message = "Found.";
            return response;
        });
    }
    
    public async Task<Response<Cosmetic>> GetCosmeticByName(string cosmeticName)
    {
        return await Task.Run(() =>
        {
            var response = new Response<Cosmetic>();

            var data = _dbContext.Cosmetics.Query().Where(x => x.Name == cosmeticName).FirstOrDefault();
            response.Data = data;
            if (data is null)
            {
                response.Message = "Not Found.";
                return response;
            }
            response.Message = "Found.";
            return response;
        });
    }
    
    public async Task<Response<bool>> AddCosmetic(AddCosmeticDto dto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();
            var gameExists = _dbContext.Games.Query().Where(x => x.Name == dto.GameName).Exists();
            if (!gameExists)
            {
                response.Data = false;
                response.Message = $"No Game found with the name {dto.GameName}.";
                return response;
            }

            if (_dbContext.Cosmetics.Query().Where(x => x.Name == dto.Name).Exists())
            {
                response.Data = false;
                response.Message = $"Cosmetic Already exists with the name {dto.Name}.";
                return response;
            }
            
            var success = _dbContext.Cosmetics.Insert(_mapper.Map<Cosmetic>(dto));
            if (success is null)
            {
                response.Data = false;
                response.Message = "Cant Insert the Cosmetic.";
                return response;
            }
            response.Data = true;
            response.Message = "Successfully Added.";
            return response;
        });
    }
    
    public async Task<Response<Cosmetic>> UpdateCosmetic(string cosmeticName, UpdateCosmeticDto dto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<Cosmetic>();
            var gameExists = _dbContext.Games.Query().Where(x => x.Name == dto.GameName).Exists();
            var cosmetics = _dbContext.Cosmetics;
            
            if (!gameExists)
            {
                response.Data = null;
                response.Message = $"No Game found with the name {dto.GameName}.";
                return response;
            }

            var entity = cosmetics.Query().Where(x => x.Name == cosmeticName).FirstOrDefault();
            if (entity is null)
            {
                response.Data = null;
                response.Message = $"No Cosmetic found with the name {cosmeticName}.";
                return response;
            }

            entity.Description = dto.Description;
            entity.GameName = dto.GameName;
            entity.Tags = dto.Tags;
            entity.PosterUrl = dto.PosterUrl;
            entity.ImageUrls = dto.ImageUrls;

            var success = cosmetics.Update(entity);
            
            if (!success)
            {
                response.Data = null;
                response.Message = "Cant Update the Cosmetic.";
                return response;
            }
            response.Data = entity;
            response.Message = "Successfully Updated.";
            return response;
        });
    }
    
    public async Task<Response<int>> DeleteCosmetic(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<int>();
            var query = _dbContext.Cosmetics.DeleteMany(x => x.Id == id);
            response.Data = query;
            if (query <= 0)
            {
                response.Message = "Already Deleted or Cosmetic Doesn't Exists.";
                return response;
            }
            response.Message = "Successfully Deleted.";
            return response;
        });
    }
    
}