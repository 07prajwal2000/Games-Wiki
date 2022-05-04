using GamesApi.Filters;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiKeysController : ControllerBase
{
    private readonly IApiKeyServices _services;

    public ApiKeysController(IApiKeyServices services)
    {
        _services = services;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult> GetApiKey(string email)
    {
        var data = await _services.GetApiKey(email);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [MasterApiKeyFilter]
    [HttpGet("All")]
    public async Task<ActionResult> GetApiKeys(int limit, int skip)
    {
        var data = await _services.GetApiKeys(limit, skip);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult> RegisterApiKey(RegisterApiKey dto)
    {
        var data = await _services.RegisterApiKey(dto);
        return data.Data is null ? NotFound(data) : Ok(data);
    }

    [MasterApiKeyFilter]
    [HttpDelete("Block/{email}")]
    public async Task<ActionResult> BlockApiKey(string email)
    {
        var data = await _services.BlockApiKey(email);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [MasterApiKeyFilter]
    [HttpPut("UnBlock/{email}")]
    public async Task<ActionResult> UnBlockApiKey(string email)
    {
        var data = await _services.UnBlockApiKey(email);
        return data.Data is null ? NotFound(data) : Ok(data);
    }

    [MasterApiKeyFilter]
    [HttpPut("Renewal/{email}")]
    public async Task<ActionResult> RenewalApiKey(string email)
    {
        var data = await _services.RenewalApiKey(email);
        return data.Data is null ? NotFound(data) : Ok(data);
    }

    [ApiKeyFilter]
    [HttpPut("Refresh/{email}")]
    public async Task<ActionResult> RefreshApiKey(string email)
    {
        var data = await _services.RefreshApiKey(email);
        return data.Data is null ? NotFound(data) : Ok(data);
    }

}