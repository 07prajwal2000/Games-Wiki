using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using EFCoreLearning.Services.IServices;
using LiteDB;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemRequirementsController : ControllerBase
{
    private readonly ISystemRequirementsServices _services;

    public SystemRequirementsController(ISystemRequirementsServices services)
    {
        _services = services;
    }
    
    [HttpGet("{gameName}")]
    public async Task<IActionResult> GetSystemRequirements(string gameName)
    {
        var result = await _services.GetSystemRequirementForGame(gameName);
        return result.Data is null ? NotFound(result) : Ok(result);
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddSystemRequirements(AddSystemRequirementDto dto)
    {
        var result = await _services.AddSystemRequirementForGame(dto);
        return !result.Data ? NotFound(result) : Ok(result);
    }
    
    [HttpPut("Update/{gameName}")]
    public async Task<IActionResult> UpdateSystemRequirements(string gameName, UpdateSystemRequirementDto dto)
    {
        var result = await _services.UpdateSystemRequirementForGame(gameName, dto);
        return result.Data is null ? NotFound(result) : Ok(result);
    }
    
    [HttpDelete("Delete/{gameName}")]
    public async Task<IActionResult> DeleteSystemRequirements(string gameName)
    {
        var result = await _services.DeleteSystemRequirementForGame(gameName);
        return !result.Data ? NotFound(result) : Ok(result);
    }

}