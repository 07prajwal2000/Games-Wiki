using GamesApi.Models;
using LiteDB;

namespace GamesApi.Data;

public class LiteDbContext
{
    public readonly ILiteCollection<Game> Games;
    public readonly ILiteCollection<GameCharacter> GameCharacters;
    public readonly ILiteCollection<SystemRequirement> SystemRequirements;
    public readonly ILiteCollection<Cosmetic> Cosmetics;
    public readonly ILiteCollection<ApiKey> ApiKeys;
    public readonly ILiteCollection<Vehicle> Vehicles;

    public LiteDbContext(ILiteDatabase liteDatabase)
    {
        Games = liteDatabase.GetCollection<Game>();
        GameCharacters = liteDatabase.GetCollection<GameCharacter>();
        SystemRequirements = liteDatabase.GetCollection<SystemRequirement>();
        Cosmetics = liteDatabase.GetCollection<Cosmetic>();
        ApiKeys = liteDatabase.GetCollection<ApiKey>();
        Vehicles = liteDatabase.GetCollection<Vehicle>();
    }
}