using AutoMapper;
using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using EFCoreLearning.Services.IServices;
using LiteDB;

namespace EFCoreLearning.Services;

public class GameCharacterServices : IGameCharacterServices
{
    private readonly IMapper _mapper;
    private readonly ILiteDatabase _liteDatabase;

    public GameCharacterServices(IMapper mapper, ILiteDatabase liteDatabase)
    {
        _mapper = mapper;
        _liteDatabase = liteDatabase;
    }

    public async Task<Response<List<GameCharacter>>> GetAllGameCharacters(int limit = 10, int skipCount = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<List<GameCharacter>>();

            var gameCharacters = _liteDatabase.GetCollection<GameCharacter>()
                .Query()
                .Skip(skipCount)
                .Limit(limit)
                .ToList();

            response.Data = gameCharacters;
            response.Message = gameCharacters.Count > 0 ? "Found." : "No Characters Found";
            return response;
        });
    }
    
    public async Task<Response<List<string>>> GetAllGameCharacterNamesByGameName(string gameName, int limit = 10, int skipCount = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<List<string>>();

            var gameCharacters = _liteDatabase.GetCollection<GameCharacter>()
                .Query()
                .Where(x => x.GameName == gameName)
                .Select(x => x.CharacterName)
                .Skip(skipCount)
                .Limit(limit)
                .ToList();

            response.Data = gameCharacters;
            response.Message = gameCharacters.Count > 0 ? "Found." : "No Characters Found";
            return response;
        });
    }

    public async Task<Response<GameCharacter?>> GetGameCharacterById(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<GameCharacter?>();

            var gameCharacter = _liteDatabase.GetCollection<GameCharacter>()
                .Query()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            
            response.Data = gameCharacter;
            response.Message = gameCharacter is null ? "Not found." : "Found.";

            return response;
        });
    }
        
    public async Task<Response<List<GameCharacter>>> GetGameCharacterByGameName(string gameName)
    {
        return await Task.Run(() =>
        {
            var response = new Response<List<GameCharacter>>();

            var gameCharacters = _liteDatabase.GetCollection<GameCharacter>()
                .Query()
                .Where(x => x.GameName == gameName)
                .ToList();

            response.Data = gameCharacters;
            response.Message = gameCharacters.Count > 0 ? "Found." : "Not Found.";
 
            return response;
        });
    }

    public async Task<Response<bool>> AddGameCharacterById(AddGameCharacterDto gameCharacterDto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();

            var gameCharacterCollection = _liteDatabase.GetCollection<GameCharacter>();
            var games = _liteDatabase.GetCollection<Game>();

            if (gameCharacterCollection.Query().Where(x => x.CharacterName == gameCharacterDto.CharacterName).Exists())
            {
                response.Data = false;
                response.Message = "Game Character Already Exists with Name '" + gameCharacterDto.CharacterName + "' ";
                return response;
            }

            if (games.Query().Where(x => x.Name == gameCharacterDto.GameName).ToList().Count <= 0)
            {
                response.Data = false;
                response.Message = "No Game Exists with " + gameCharacterDto.GameName;
                return response;
            }

            var character = _mapper.Map<GameCharacter>(gameCharacterDto);
            gameCharacterCollection.Insert(character);
            response.Data = true;
            response.Message = "Successfully Added.";
            return response;
        });
    }
        
    public async Task<Response<GameCharacter?>> UpdateGameCharacterById(int id, AddGameCharacterDto updateGameCharacterDto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<GameCharacter?>();

            var gameCharacterCollection = _liteDatabase.GetCollection<GameCharacter>();

            var character = gameCharacterCollection.Query()
                .Where(x => x.Id == id);

            var updateChar = character.FirstOrDefault();

            var games = _liteDatabase.GetCollection<Game>();

            if (gameCharacterCollection.Query().Where(x => x.CharacterName == updateGameCharacterDto.CharacterName).Exists())
            {
                response.Data = null;
                response.Message = "No Game Character Exists with " + updateGameCharacterDto.CharacterName;
                return response;
            }

            if (games.Query().Where(x => x.Name == updateGameCharacterDto.GameName).ToList().Count <= 0)
            {
                response.Data = null;
                response.Message = "No Game Exists with " + updateGameCharacterDto.GameName;
                return response;
            }

            updateChar.CharacterName = updateGameCharacterDto.CharacterName;
            updateChar.AboutCharacter = updateGameCharacterDto.AboutCharacter;
            updateChar.GameName = updateGameCharacterDto.GameName;
            updateChar.ImageUrl = updateGameCharacterDto.ImageUrl;
                
            var updated = gameCharacterCollection.Update(updateChar);

            if (!updated)
            {
                response.Data = null;
                response.Message = "Something Went Wrong";
                return response;
            }

            response.Data = updateChar;
            response.Message = "Found";

            return response;
        });
    }

    public async Task<Response<bool>> DeleteGameCharacterById(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();

            var gameCharacterCollection = _liteDatabase.GetCollection<GameCharacter>();
            var count = gameCharacterCollection.DeleteMany(x => x.Id == id);
            if (count <= 0)
            {
                response.Data = false;
                response.Message = "Id not found";
                return response;
            }

            response.Data = true;
            response.Message = "Successfully Deleted";
            return response;
        });
    }

}