namespace GamesApi.Models.Dtos;

public record class AddVehicleDto
{
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

public record class UpdateVehicleDto
{
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