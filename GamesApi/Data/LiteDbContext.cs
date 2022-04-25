﻿using GamesApi.Models;
using LiteDB;

namespace GamesApi.Data;

public class LiteDbContext
{
    public readonly ILiteCollection<Game> Games;
    public readonly ILiteCollection<GameCharacter> GameCharacters;
    public readonly ILiteCollection<SystemRequirement> SystemRequirements;

    public LiteDbContext(ILiteDatabase liteDatabase)
    {
        Games = liteDatabase.GetCollection<Game>();
        GameCharacters = liteDatabase.GetCollection<GameCharacter>();
        SystemRequirements = liteDatabase.GetCollection<SystemRequirement>();
    }
}