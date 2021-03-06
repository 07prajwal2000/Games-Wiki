using GamesApi.Filters;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyFilter]
public class GameCharacterController : ControllerBase
{
    private readonly IGameCharacterServices _characterServices;

    public GameCharacterController(IGameCharacterServices characterServices)
    {
        _characterServices = characterServices;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAllCharacters([FromQuery]int limit = 10, [FromQuery]int skipCount = 0)
    {
        var characters = await _characterServices.GetAllGameCharacters(limit, skipCount);
        return characters.Data is {Count: > 0} ? Ok(characters) : NotFound(characters);
    }

    [HttpGet("GetNames/{gameName}")]
    public async Task<IActionResult> GetAllCharacterNames(string gameName, [FromQuery]int limit = 10, [FromQuery]int skipCount = 0)
    {
        if (limit > 50)
        {
            return BadRequest(new Response<string>{Message = "Limit exceeded over 50"});
        }
        var characters = await _characterServices.GetAllGameCharacterNamesByGameName(gameName, limit, skipCount);
        return characters.Data is {Count: > 0} ? Ok(characters) : NotFound(characters);
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetCharacter(int id)
    {
        var character = await _characterServices.GetGameCharacterById(id);
        return character.Data is not null ? Ok(character) : NotFound(character);
    }
        
    [HttpGet("Get/{gameName}")]
    public async Task<IActionResult> GetCharacter(string gameName)
    {
        var character = await _characterServices.GetGameCharacterByGameName(gameName);
        return character.Data is {Count: > 0} ? Ok(character) : NotFound(character);
    }

    [MasterApiKeyFilter]
    [HttpPost("Add")]
    public async Task<IActionResult> GetCharacter(AddGameCharacterDto characterDto)
    {
        var success = await _characterServices.AddGameCharacterById(characterDto);
        return success.Data ? Ok(success) : NotFound(success);
    }

    [MasterApiKeyFilter]
    [HttpPut("Update/{id:int}")]
    public async Task<IActionResult> UpdateCharacter(int id, AddGameCharacterDto characterDto)
    {
        var character = await _characterServices.UpdateGameCharacterById(id, characterDto);
        return character.Data is not null? Ok(character) : NotFound(character);
    }

    [MasterApiKeyFilter]
    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var success = await _characterServices.DeleteGameCharacterById(id);
        return success.Data ? Ok(success) : NotFound(success);
    }
}