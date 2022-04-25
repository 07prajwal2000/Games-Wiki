using AutoMapper;
using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using EFCoreLearning.Services.IServices;
using LiteDB;

namespace EFCoreLearning.Services
{
    public class SystemRequirementsServices : ISystemRequirementsServices
    {
        private readonly ILiteDatabase _liteDatabase;
        private readonly IMapper _mapper;

        public SystemRequirementsServices(ILiteDatabase liteDatabase, IMapper mapper)
        {
            _liteDatabase = liteDatabase;
            _mapper = mapper;
        }
        
        public async Task<Response<SystemRequirement>> GetSystemRequirementForGame(string gameName)
        {
            return await Task.Run(() =>
            {
                var response = new Response<SystemRequirement>();
                var collections = _liteDatabase.GetCollection<SystemRequirement>();
                var sysReq = collections.Query().Where(x => x.GameName == gameName).FirstOrDefault();
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
                var gameExists = _liteDatabase.GetCollection<Game>().Query().Where(x => x.Name == dto.GameName).Exists();
                var sysCol = _liteDatabase.GetCollection<SystemRequirement>();
                
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
                var gameExists = _liteDatabase.GetCollection<Game>().Query().Where(x => x.Name == gameName).Exists();
                var sysCol = _liteDatabase.GetCollection<SystemRequirement>();
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
                var gameExists = _liteDatabase.GetCollection<Game>().Query().Where(x => x.Name == gameName).Exists();
                var sysCol = _liteDatabase.GetCollection<SystemRequirement>();
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
}
