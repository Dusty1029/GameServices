using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SteamBL(IGameRepository gameRepository, ISteamApiGateway steamApiGateway) : ISteamBL
    {
        public async Task<Guid> AddSteamGame(SteamGameDto gameSteamDto)
        {
            var achievementsResult = steamApiGateway.GetAchievementByAppId(gameSteamDto.SteamId);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(gameSteamDto.SteamId);
            await Task.WhenAll(achievementsResult, percentagesResult);

            var game = await gameRepository.InsertAndSave(gameSteamDto.ToEntity(achievementsResult.Result, percentagesResult.Result));

            return game.Id;
        }

        public async Task<List<SteamGameDto>> GetMissingSteamGames()
        {
            var steamIds = await gameRepository.GetSelect(f => f.Select(g => g.SteamId), g => g.SteamId != null);
            var steamGames = await steamApiGateway.GetSteamGames();

            return steamGames?.Where(sg => !steamIds.Contains(sg.appid)).OrderBy(sg => sg.name).Select(sg => sg.ToDto()).ToList() ?? [];
        }

        public async Task<Guid> IgnoreSteamGame(SteamGameDto gameSteamDto)
        {
            var gameEntity = gameSteamDto.ToEntity();
            gameEntity.IsIgnored = true;
            await gameRepository.InsertAndSave(gameEntity);

            return gameEntity.Id;
        }

        public async Task ReloadSteamGame(Guid gameId)
        {
            var game = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Achievements), noTracking: false);
            if (game is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");
            if (!game.SteamId.HasValue)
                throw new ValidationException($"The game with id [{gameId}] has no steamId and can't be reloaded.");

            var achievementsResult = steamApiGateway.GetAchievementByAppId(game.SteamId.Value);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(game.SteamId.Value);
            await Task.WhenAll(achievementsResult, percentagesResult);

            game.Achievements?.ForEach(a =>
            {
                a.Achieved = achievementsResult.Result.FirstOrDefault(ac => ac.apiname == a.SteamName)?.achieved != 0;
                a.Percentage = percentagesResult.Result.FirstOrDefault(p => p.name == a.SteamName)?.percent;
            });

            await gameRepository.SaveChanges();
        }
    }
}
