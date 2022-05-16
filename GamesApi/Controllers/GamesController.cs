using GamesApi.Filters;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiKeyFilter]
public class GamesController : ControllerBase
{
    private readonly IGamesServices _gamesServices;

    public GamesController(IGamesServices gamesServices)
    {
        _gamesServices = gamesServices;
    }
    
    [HttpGet("Games")]
    public async Task<ActionResult> GetGames([FromQuery] int limit = 10, [FromQuery] int skipCount = 0)
    {
        var res = await _gamesServices.GetGamesByLimit(limit, skipCount);
        var resCount = res.Data!.Count();
        return resCount > 0 ? Ok(res) : NotFound(res);
    }

    [HttpGet("Names")]
    public async Task<ActionResult> GetGameNames([FromQuery] int limit = 10, [FromQuery] int skipCount = 0)
    {
        if (limit > 50)
        {
            return BadRequest(new Response<string>{Message = "Limit exceeded over 50"});
        }
        var res = await _gamesServices.GetGameNamesByLimit(limit, skipCount);
        var resCount = res.Data!.Count();
        return resCount > 0 ? Ok(res) : NotFound(res);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetGame(int id)
    {
        var res = await _gamesServices.GetGame(id);
        return res.Data is not null ? Ok(res) : NotFound(res);
    }
    
    [MasterApiKeyFilter]
    [HttpPost("Add")]
    public async Task<ActionResult> AddGame([FromBody] AddGame gameDto)
    {
        var res = await _gamesServices.AddGame(gameDto);
        return res.Data ? Ok(res) : NotFound(res);
    }

    [MasterApiKeyFilter]
    [HttpPut("Update/{id:int}")]
    public async Task<ActionResult> AddGame(int id, [FromBody] AddGame gameDto)
    {
        var res = await _gamesServices.UpdateGame(id, gameDto);
        return res.Data is null ? NotFound(res) : Ok(res);
    }

    [MasterApiKeyFilter]
    [HttpDelete("Delete/{id:int}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var res = await _gamesServices.DeleteGame(id);
        return res.Data ? Ok(res) : NotFound(res);
    }
}