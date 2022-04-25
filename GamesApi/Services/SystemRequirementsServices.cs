using AutoMapper;
using GamesApi.Data;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;

namespace GamesApi.Services;

public class SystemRequirementsServices : ISystemRequirementsServices
{
    private readonly IMapper _mapper;
    private readonly LiteDbContext _dbContext;

    public SystemRequirementsServices(IMapper mapper, LiteDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Response<SystemRequirement>> GetSystemRequirementForGame(string gameName)
    {
        return await Task.Run(() =>
        {
            var response = new Response<SystemRequirement>();
            var collections = _dbContext.SystemRequirements;
            var sysReq = collections.Query().Where(x => x.GameName == gameName).FirstOrDefault();
            response.Data = sysReq;
            response.Message = sysReq is not null ? "Found." : "No Data Found.";
            return response;
        });
    }
        
    public async Task<Response<SystemRequirement>> GetSystemRequirementForGameById(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<SystemRequirement>();
            var collections = _dbContext.SystemRequirements;
            var sysReq = collections.Query().Where(x => x.Id == id).FirstOrDefault();
            response.Data = sysReq;
            response.Message = sysReq is not null ? "Found." : "No Data Found.";
            return response;
        });
    }
        
    public async Task<Response<bool>> AddSystemRequirementForGame(AddSystemRequirementDto dto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();
            var gameExists = _dbContext.Games.Query().Where(x => x.Name == dto.GameName).Exists();
            var sysCol = _dbContext.SystemRequirements;
                
            if (!gameExists || sysCol.Query().Where(x => x.GameName == dto.GameName).Exists())
            {
                response.Data = false;
                response.Message = $"System Requirements already exists with the {dto.GameName} Game.";
                return response;
            }

            sysCol.Insert(_mapper.Map<SystemRequirement>(dto));
            response.Data = true;
            response.Message = "Successfully Added.";
            return response;
        });
    }
        
    public async Task<Response<SystemRequirement>> UpdateSystemRequirementForGame(string gameName, UpdateSystemRequirementDto dto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<SystemRequirement>();
            var gameExists = _dbContext.Games.Query().Where(x => x.Name == gameName).Exists();
            var sysCol = _dbContext.SystemRequirements;
            var entity = sysCol.Query().Where(x => x.GameName == gameName).FirstOrDefault();
                
            if (!gameExists || entity is null)
            {
                response.Data = null;
                response.Message = $"System Requirements not found with the {gameName} Game.";
                return response;
            }

            entity.Ram = dto.Ram;
            entity.Storage = dto.Storage;
            entity.GraphicsCard = dto.GraphicsCard;
            entity.OperatingSystem = dto.OperatingSystem;
            entity.RequiresInternet = dto.RequiresInternet;
            entity.Processor = dto.Processor;

            sysCol.Update(entity);
            response.Data = entity;
            response.Message = "Successfully Updated.";
            return response;
        });
    }
        
    public async Task<Response<bool>> DeleteSystemRequirementForGame(string gameName)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();
            var gameExists = _dbContext.Games.Query().Where(x => x.Name == gameName).Exists();
            var sysCol = _dbContext.SystemRequirements;
            var entity = sysCol.Query().Where(x => x.GameName == gameName).FirstOrDefault();
                
            if (!gameExists || entity is null)
            {
                response.Data = false;
                response.Message = $"System Requirements not found with the {gameName} Game.";
                return response;
            }

            var success = sysCol.DeleteMany(x => x.GameName == gameName);
            response.Data = true;
            response.Message = success > 0 ? "Successfully Deleted." : "Deletion Failed.";
            return response;
        });
    }
        
}