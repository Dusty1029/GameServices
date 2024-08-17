using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos.SteamGateway;
using GameServices.API.Extensions.Entities;
using GameServices.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameServices.API.BusinessLogics.Implementations
{
    public class SteamBL : ISteamBL
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISteamApiGateway _steamApiGateway;

        public SteamBL(IGameRepository gameRepository, ISteamApiGateway steamApiGateway)
        {
            _gameRepository = gameRepository;
            _steamApiGateway = steamApiGateway;
        }

        public async Task<Guid> AddSteamGame(GameSteamDto gameSteamDto)
        {
            var achievementsResult = _steamApiGateway.GetAchievementByAppId(gameSteamDto.appid);
            var percentagesResult = _steamApiGateway.GetAchievementPercentageByAppId(gameSteamDto.appid);
            await Task.WhenAll(achievementsResult, percentagesResult);

            var game = await _gameRepository.InsertAndSave(gameSteamDto.ToEntity(achievementsResult.Result, percentagesResult.Result));

            return game.Id;
        }

        public async Task<List<GameSteamDto>?> GetMissingSteamGames()
        {
            var steamIds = await _gameRepository.GetSelect(f => f.Select(g => g.SteamId), g => g.SteamId != null);
            var steamGames = await _steamApiGateway.GetSteamGames();

            return steamGames?.Where(sg => !steamIds.Contains(sg.appid)).OrderBy(sg => sg.name).ToList();
        }

        public async Task<Guid> IgnoreSteamGame(GameSteamDto gameSteamDto)
        {
            var gameEntity = gameSteamDto.ToEntity();
            gameEntity.IsIgnored = true;
            await _gameRepository.InsertAndSave(gameEntity);

            return gameEntity.Id;
        }

        public async Task ReloadSteamGame(Guid gameId)
        {
            var game = await _gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Achievements), noTracking: false);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");
            if (!game.SteamId.HasValue)
                throw new ValidationException($"The game with id [{gameId}] has no steamId and can't be reloaded.");

            var achievementsResult = _steamApiGateway.GetAchievementByAppId(game.SteamId.Value);
            var percentagesResult = _steamApiGateway.GetAchievementPercentageByAppId(game.SteamId.Value);
            await Task.WhenAll(achievementsResult, percentagesResult);

            game.Achievements?.ForEach(a =>
            {
                a.Achieved = achievementsResult.Result.FirstOrDefault(ac => ac.apiname == a.SteamName)?.achieved != 0;
                a.Percentage = percentagesResult.Result.FirstOrDefault(p => p.name == a.SteamName)?.percent;
            });

            await _gameRepository.SaveChanges();
        }
    }
}
