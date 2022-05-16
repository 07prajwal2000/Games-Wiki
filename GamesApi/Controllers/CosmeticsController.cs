using GamesApi.Filters;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiKeyFilter]
public class CosmeticsController : ControllerBase
{
    private readonly ICosmeticsServices _services;

    public CosmeticsController(ICosmeticsServices services)
    {
        _services = services;
    }

    [HttpGet("ById/{id}")]
    public async Task<IActionResult> GetCosmetics(int id)
    {
        var data = await _services.GetCosmeticById(id);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> GetCosmetics(string name)
    {
        var data = await _services.GetCosmeticByName(name);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCosmetics([FromQuery]int limit = 10, [FromQuery]int skip = 0)
    {
        if (limit > 50)
        {
            return BadRequest(new Response<string>{Message = "Limit exceeded over 50"});
        }
        var data = await _services.GetCosmetics(limit, skip);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [HttpPost("Add")]
    [MasterApiKeyFilter]
    public async Task<IActionResult> AddCosmetic(AddCosmeticDto dto)
    {
        var data = await _services.AddCosmetic(dto);
        return !data.Data ? NotFound(data) : Ok(data);
    }
    
    [HttpPut("Update/{name}")]
    [MasterApiKeyFilter]
    public async Task<IActionResult> UpdateCosmetic(string name, UpdateCosmeticDto dto)
    {
        var data = await _services.UpdateCosmetic(name, dto);
        return data.Data is null ? NotFound(data) : Ok(data);
    }
    
    [HttpDelete("Delete/{id}")]
    [MasterApiKeyFilter]
    public async Task<IActionResult> DeleteCosmetic(int id)
    {
        var data = await _services.DeleteCosmetic(id);
        return data.Data <= 0 ? NotFound(data) : Ok(data);
    }
    
}