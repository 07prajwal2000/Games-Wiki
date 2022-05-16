using GamesApi.Data;
using GamesApi.Models;
using GamesApi.Services.IServices;

namespace GamesApi.Services;

public class VehicleServices : IVehicleServices
{
	private readonly LiteDbContext _context;

	public VehicleServices(LiteDbContext context)
	{
		_context = context;
	}

	public async Task<Response<Vehicle>> GetVehicle(int id)
	{
		return await Task.Run(() =>
		{
			var response = new Response<Vehicle>();
			var entity = _context.Vehicles.Query().Where(x => x.Id == id).FirstOrDefault();
			response.Data = entity;
			response.Message = entity is null ? "Not found" : "Success";
			return response;
		});
	}
	
	public async Task<Response<List<Vehicle>>> GetVehicles(int limit, int skip)
	{
		return await Task.Run(() =>
		{
			var response = new Response<List<Vehicle>>();
			var entities = _context.Vehicles.Query().Skip(skip).Limit(limit).ToList();
			response.Data = entities;
			response.Message = entities is null ? "Not found" : "Success";
			return response;
		});
	}
	
}