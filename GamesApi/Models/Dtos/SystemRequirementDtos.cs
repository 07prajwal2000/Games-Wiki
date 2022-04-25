namespace EFCoreLearning.Models.Dtos;

public record AddSystemRequirementDto(
    string GameName,
    string Processor,
    string OperatingSystem, 
    string GraphicsCard,
    string Ram, 
    string Storage, 
    bool RequiresInternet); 

public record UpdateSystemRequirementDto(
    string OperatingSystem, 
    string GraphicsCard,
    string Processor,
    string Ram, 
    string Storage, 
    bool RequiresInternet); 