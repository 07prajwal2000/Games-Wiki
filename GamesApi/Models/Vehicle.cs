namespace GamesApi.Models;

public class Vehicle
{
	public int Id { get; set; }
	public string ModelName { get; set; } = "";
	public string Description { get; set; } = "";
	public VehicleType VehicleType { get; set; }
	public string Game { get; set; } = "";
	public uint PassengerCapacity { get; set; } = 1;
	public string ImageUrl { get; set; } = "";
	public List<string> ScreenshotUrls { get; set; } = new();
	public uint PriceInUsd { get; set; } = 0;
	public uint TopSpeedInKmh { get; set; }
	public decimal TopSpeedInMph => (decimal) (TopSpeedInKmh * 0.621371);

	public VehicleManufacturer Manufacturer { get; set; }
	public List<VehicleAbility>? Abilities { get; set; }
	public List<VehicleAttachment>? Attachments { get; set; }
	public List<string>? Tags { get; set; }
}

public class VehicleManufacturer
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string LogoUrl { get; set; }
}

public class VehicleAbility
{
	public string Name { get; set; } = "";
	public string Description { get; set; } = "";
	public string? VideoUrl { get; set; }
	public string ImageUrl { get; set; } = "";
}

public class VehicleAttachment : VehicleAbility
{
	public uint Cost { get; set; }
	public string Category { get; set; } = "";
}

public enum VehicleType
{
	SedanCar, SportsCar, HatchBack, Van, Suv, Jeep, // CARS
	Street, Cruiser, SportBike, OffRoad, Scooter, Bicycle,// Bikes
	PickupTruck, Tractor, TowTruck, FireEngine, CementMixture, Tanker, // Trucks
	Airplane, Helicopter, AirBalloons, Blimps, Gliders, // AIR Vehicles 
	MotorBoat, CargoShip, SpeedBoat, SailBoat, Yacht, BattleShip, CruiseShip, JetSki, Submarine, // Boat and Ship
	
	Railways, Military, Miscellaneous // Others
}