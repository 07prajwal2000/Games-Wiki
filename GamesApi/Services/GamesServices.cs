using AutoMapper;
using GamesApi.Data;
using GamesApi.Models;
using GamesApi.Models.Dtos;
using GamesApi.Services.IServices;

namespace GamesApi.Services;

public class GamesServices : IGamesServices
{
    private readonly IMapper _mapper;
    private readonly LiteDbContext _dbContext;

    public GamesServices(IMapper mapper, LiteDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<Response<IEnumerable<Game>>> GetGamesByLimit(int limit = 10, int skipCount = 0)
    {
        return await Task.Run(() =>
        {
            var response = new Response<IEnumerable<Game>>();
            if (limit > 50)
            {
                response.Data = null;
                response.Message = "The Limit QueryParam has GreaterThan 50.";
                return response;
            }
            var games = _dbContext.Games.Query().Skip(skipCount).Limit(limit).ToList();

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

            var games = _dbContext.Games
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

            var game = _dbContext.Games.Query()
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

            var games = _dbContext.Games;

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

            var games = _dbContext.Games;
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
            int deleted = _dbContext.Games.DeleteMany(game => game.Id == gameId);
            var condition = deleted > 0;
            response.Data = condition;
            response.Message = condition ? "Successfully Deleted." : "No Game with Id: " + gameId + " Found.";
            return response;
        });
    }

}