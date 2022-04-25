namespace GamesApi.Models.Dtos;

public record AddGame(string Name, string Description, string Developer, 
    decimal Price, int Discount, string? GamePosterUrl, 
    string? GameTrailerUrl, List<string> GameTags);
