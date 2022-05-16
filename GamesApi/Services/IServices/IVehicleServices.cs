using GamesApi.Models;

namespace GamesApi.Services.IServices;

public interface IVehicleServices
{
	Task<Response<Vehicle>> GetVehicle(int id);
	Task<Response<List<Vehicle>>> GetVehicles(int limit, int skip);
}