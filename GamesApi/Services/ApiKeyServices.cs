using AutoMapper;
using GamesApi.Data;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;

namespace GamesApi.Services;

public class ApiKeyServices : IApiKeyServices
{
    private readonly LiteDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApiKeyServices(LiteDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<List<ApiKey>>> GetApiKeys(int limit, int skip)
    {
        return await Task.Run(() =>
        {
            var response = new Response<List<ApiKey>>();
            
            var collection = _dbContext.ApiKeys.Query().Skip(skip).Limit(limit).ToList();
            response.Data = collection;
            response.Message = collection is not null ? "Success." : "No keys found.";
            return response;
        });
    }

    public async Task<Response<ApiKey>> RefreshApiKey(string email)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            var apiKey = _dbContext.ApiKeys.Query().Where(x => x.Email == email).FirstOrDefault();
            response.Data = apiKey;

            if (apiKey is null)
            {
                response.Message = "No Key Found.";
                return response;
            }
            apiKey.Key = Guid.NewGuid();
            var success = _dbContext.ApiKeys.Update(apiKey);
            response.Message = success ? "Refreshed." : "Something went wrong.";
            return response;
        });
    }
    
    public async Task<Response<ApiKey>> RegisterApiKey(RegisterApiKey dto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            var apiKey = _mapper.Map<ApiKey>(dto);

            if (_dbContext.ApiKeys.Query().Where(x => x.Email == dto.Email).Exists())
            {
                response.Data = null;
                response.Message = "Email Already Exists.";
                return response;
            }
            
            apiKey.Key = Guid.NewGuid();
            apiKey.ValidTill = DateTime.Now.AddMonths(1);
            
            var collection = _dbContext.ApiKeys.Insert(apiKey);
            response.Data = collection is not null ? apiKey : null;
            response.Message = collection is not null ? "Successfully Registered Your Key" : "Something went wrong.";
            return response;
        });
    }

    public async Task<Response<ApiKey>> GetApiKey(string email)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            
            var collection = _dbContext.ApiKeys.Query().Where(x => x.Email == email).FirstOrDefault();
            response.Data = collection;
            response.Message = collection is not null ? "Success." : "No key found.";
            return response;
        });
    }

    public async Task<Response<ApiKey>> BlockApiKey(string email)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            var apiKey = _dbContext.ApiKeys.Query().Where(x => x.Email == email).FirstOrDefault();
            response.Data = apiKey;

            if (apiKey is null)
            {
                response.Message = "No Key Found.";
                return response;
            }
            
            apiKey.Blocked = true;
            apiKey.ValidTill = DateTime.Now;
            var success = _dbContext.ApiKeys.Update(apiKey);
            response.Message = success ? "Blocked." : "Something went wrong.";
            return response;
        });
    }

    public async Task<Response<ApiKey>> UnBlockApiKey(string email)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            var apiKey = _dbContext.ApiKeys.Query().Where(x => x.Email == email).FirstOrDefault();
            response.Data = apiKey;

            if (apiKey is null)
            {
                response.Message = "No Key Found.";
                return response;
            }
            
            apiKey.Blocked = false;
            var success = _dbContext.ApiKeys.Update(apiKey);
            response.Message = success ? "UnBlocked." : "Something went wrong.";
            return response;
        });
    }

    public async Task<Response<ApiKey>> RenewalApiKey(string email)
    {
        return await Task.Run(() =>
        {
            var response = new Response<ApiKey>();
            var apiKey = _dbContext.ApiKeys.Query().Where(x => x.Email == email).FirstOrDefault();
            response.Data = apiKey;

            if (apiKey is null)
            {
                response.Message = "No Key Found.";
                return response;
            }
            
            apiKey.ValidTill = DateTime.Now.AddMonths(1);
            var success = _dbContext.ApiKeys.Update(apiKey);
            response.Message = success ? "Validity extended to one month from now." : "Something went wrong.";
            return response;
        });
    }
    
}