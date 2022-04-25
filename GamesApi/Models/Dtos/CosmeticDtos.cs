namespace GamesApi.Models.Dtos;

public record AddCosmeticDto(
    string Name, 
    string Description, 
    string GameName, 
    string PosterUrl, 
    List<string>? Tags,
    List<string>? ImageUrls
);
public record UpdateCosmeticDto(
    string Description, 
    string GameName, 
    string PosterUrl, 
    List<string>? Tags,
    List<string>? ImageUrls
);