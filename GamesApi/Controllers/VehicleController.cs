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
	
	[HttpGet("/test2")]
	public IActionResult Test2(Vehicle v)
	{
		return Ok(new {Enums = Enum.GetNames(typeof(VehicleType)), Query = v});
	}
}