using GamesApi.Filters;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiKeyFilter]
public class SystemRequirementsController : ControllerBase
{
    private readonly ISystemRequirementsServices _services;

    public SystemRequirementsController(ISystemRequirementsServices services)
    {
        _services = services;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSystemRequirements(int id)
    {
        var result = await _services.GetSystemRequirementForGameById(id);
        return result.Data is null ? NotFound(result) : Ok(result);
    }
    
    [HttpGet("{gameName}")]
    public async Task<IActionResult> GetSystemRequirements(string gameName)
    {
        var result = await _services.GetSystemRequirementForGame(gameName);
        return result.Data is null ? NotFound(result) : Ok(result);
    }
    
    [MasterApiKeyFilter]
    [HttpPost("Add")]
    public async Task<IActionResult> AddSystemRequirements(AddSystemRequirementDto dto)
    {
        var result = await _services.AddSystemRequirementForGame(dto);
        return !result.Data ? NotFound(result) : Ok(result);
    }
    
    [MasterApiKeyFilter]
    [HttpPut("Update/{gameName}")]
    public async Task<IActionResult> UpdateSystemRequirements(string gameName, UpdateSystemRequirementDto dto)
    {
        var result = await _services.UpdateSystemRequirementForGame(gameName, dto);
        return result.Data is null ? NotFound(result) : Ok(result);
    }
    
    [MasterApiKeyFilter]
    [HttpDelete("Delete/{gameName}")]
    public async Task<IActionResult> DeleteSystemRequirements(string gameName)
    {
        var result = await _services.DeleteSystemRequirementForGame(gameName);
        return !result.Data ? NotFound(result) : Ok(result);
    }

}