using GamesApi.Filters;
using GamesApi.Models;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[ApiKeyFilter]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
	private readonly IVehicleServices _services;

	public VehicleController(IVehicleServices services)
	{
		_services = services;
	}
	
	[HttpGet("Types")]
	public IActionResult GetVehicleTypes()
	{
		return Ok(new Response<string[]>{Data = Enum.GetNames(typeof(VehicleType)), Message = "Success"});
	}
	
	[HttpGet("Vehicles")]
	public async Task<IActionResult> GetVehicles([FromQuery]int limit = 10, [FromQuery]int skip = 0)
	{
		if (limit > 100)
		{
			return BadRequest(new Response<string>{Message = "Limit exceeded over 100"});
		}
		var data = await _services.GetVehicles(limit, skip);
		return data.Data is null ? NotFound(data) : Ok(data);
	}
	
	[HttpGet("Vehicle/{id}")]
	public async Task<IActionResult> GetVehicle(int id)
	{
		var data = await _services.GetVehicle(id);
		return data.Data is null ? NotFound(data) : Ok(data);
	}
	
}