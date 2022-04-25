using System.Text.Json.Serialization;

namespace EFCoreLearning.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public string? GamePosterUrl { get; set; }
    public string? GameTrailerUrl { get; set; }

    public List<string> GameTags { get; set; } = new ();
}