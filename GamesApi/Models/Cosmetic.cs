namespace GamesApi.Models;

public class Cosmetic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string GameName { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public List<string>? Tags { get; set; }
    public List<string>? ImageUrls { get; set; }
}