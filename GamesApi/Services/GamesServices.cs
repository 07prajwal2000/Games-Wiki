using AutoMapper;
using EFCoreLearning.Models;
using EFCoreLearning.Models.Dtos;
using EFCoreLearning.Services.IServices;
using LiteDB;

namespace EFCoreLearning.Services;

public class GamesServices : IGamesServices
{
    private readonly IMapper _mapper;
    private readonly ILiteDatabase _liteDatabase;

    public GamesServices(IMapper mapper, ILiteDatabase liteDatabase)
    {
        _mapper = mapper;
        _liteDatabase = liteDatabase;
    }

    public async Task<Response<IEnumerable<Game>>> GetGamesByLimit(int limit = 10, int skipCount = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<IEnumerable<Game>>();
            var games = _liteDatabase.GetCollection<Game>().Query().Skip(skipCount).Limit(limit).ToList();

            response.Data = games;
            response.Message = games.Count > 0 ? "Found." : "Not Found.";

            return response;
        });
    }

    public async Task<Response<IEnumerable<string>>> GetGameNamesByLimit(int limit = 10, int skipCount = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<IEnumerable<string>>();

            var games = _liteDatabase.GetCollection<Game>()
                .Query()
                .Select(x => x.Name)
                .Skip(skipCount)
                .Limit(limit)
                .ToList();
            response.Data = games;
            response.Message = games.Count > 0 ? "Found." : "Not Found.";

            return response;
        });
    }

    public async Task<Response<Game?>> GetGame(int id)
    {
        return await Task.Run(() =>
        {
            var response = new Response<Game?>();

            var game = _liteDatabase.GetCollection<Game>().Query()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            response.Data = game;
            response.Message = game is null ? "Not Found." : "Found Game.";

            return response;
        });
    }
    
    public async Task<Response<bool>> AddGame(AddGame addGameDto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();

            var games = _liteDatabase.GetCollection<Game>();

            var condition = games.Query().Where(x => x.Name == addGameDto.Name).Exists();

            var addGame = _mapper.Map<Game>(addGameDto);
            games.Insert(addGame);

            response.Data = condition;
            response.Message = condition ? "Game with Name" + addGameDto.Name + " already exists." : "Successfully Added.";

            return response;
        });
    }
    
    public async Task<Response<Game?>> UpdateGame(int gameId, AddGame updateGameDto)
    {
        return await Task.Run(() =>
        {
            var response = new Response<Game?>();

            var games = _liteDatabase.GetCollection<Game>();
            var game = games.Query()
                .Where(x => x.Id == gameId)
                .FirstOrDefault();

            response.Data = game;
            
            if (game is null)
            {
                response.Message = "No game found with Id: " + gameId;
                return response;
            }

            game.Name = updateGameDto.Name;
            game.Description = updateGameDto.Description;
            game.Developer = updateGameDto.Developer;
            game.Discount = updateGameDto.Discount;
            game.Price = updateGameDto.Price;
            game.GamePosterUrl = updateGameDto.GamePosterUrl;
            game.GameTrailerUrl = updateGameDto.GameTrailerUrl;
            game.GameTags = updateGameDto.GameTags;

            games.Update(game);
            response.Message = "Successfully updated the game.";
            return response;
        });
    }

    public async Task<Response<bool>> DeleteGame(int gameId)
    {
        return await Task.Run(() =>
        {
            var response = new Response<bool>();
            int deleted = _liteDatabase.GetCollection<Game>().DeleteMany(game => game.Id == gameId);
            var condition = deleted > 0;
            response.Data = condition;
            response.Message = condition ? "Successfully Deleted." : "No Game with Id: " + gameId + " Found.";
            return response;
        });
    }

}