using GamesApi.Models;
using GamesApi.Models.Dtos;

namespace GamesApi.Services.IServices;

public interface IApiKeyServices
{
    Task<Response<ApiKey>> RegisterApiKey(RegisterApiKey dto);
    Task<Response<ApiKey>> GetApiKey(string email);
    Task<Response<ApiKey>> RefreshApiKey(string email);
    Task<Response<ApiKey>> BlockApiKey(string email);
    Task<Response<ApiKey>> UnBlockApiKey(string email);
    Task<Response<ApiKey>> RenewalApiKey(string email);
}