namespace GamesApi.Models;

public class SystemRequirement
{
    public int Id { get; set; }
    public string GameName { get; set; } = String.Empty;
    public string OperatingSystem { get; set; } = String.Empty;
    public string GraphicsCard { get; set; } = String.Empty;
    public string Ram { get; set; } = String.Empty;
    public string Storage { get; set; } = String.Empty;
    public string Processor { get; set; } = String.Empty;
    public bool RequiresInternet { get; set; }
}