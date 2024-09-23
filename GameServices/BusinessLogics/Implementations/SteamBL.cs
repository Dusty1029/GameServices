using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SteamBL(IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IIgnoredSteamGameRepository ignoredSteamGameRepository,
        IPlatformRepository platformRepository,
        IAchievementRepository achievementRepository,
        ISteamApiGateway steamApiGateway) : ISteamBL
    {
        public async Task<Guid> AddSteamGame(SteamGameDto gameSteamDto)
        {
            var steamPlatformId = platformRepository.FindSelect(p => p.PlatformEnum == PlatformEnumEntity.Steam, f => f.Select(p => p.Id));
            var achievementsResult = steamApiGateway.GetAchievementByAppId(gameSteamDto.SteamId);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(gameSteamDto.SteamId);

            await Task.WhenAll(achievementsResult, percentagesResult, steamPlatformId);

            var game = await gameRepository.InsertAndSave(gameSteamDto.ToEntity(achievementsResult.Result, percentagesResult.Result, steamPlatformId.Result));

            return game.Id;
        }

        public async Task<List<SteamGameDto>> GetMissingSteamGames()
        {
            var steamIds = await gameDetailRepository.GetSelect(f => f.Select(g => g.SteamId), g => g.SteamId != null);
            var ignoredSteamIds = ignoredSteamGameRepository.GetSelect(i => i.Select(i => (int?)i.SteamId));
            var steamGames = steamApiGateway.GetSteamGames();

            await Task.WhenAll(ignoredSteamIds, steamGames);

            return steamGames.Result?.Where(sg => !steamIds.Union(ignoredSteamIds.Result).Contains(sg.appid)).OrderBy(sg => sg.name).Select(sg => sg.ToDto()).ToList() ?? [];
        }

        public async Task<int> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var ignoredSteamGame = gameSteamDto.ToEntity();
                await ignoredSteamGameRepository.InsertAndSave(ignoredSteamGame);
            }

            return gameSteamDto.SteamId;
        }

        public async Task ReloadSteamGame(Guid gameDetailId)
        {
            var gameDetail = await gameDetailRepository.Find(g => g.Id == gameDetailId, f => f.Include(g => g.Achievements), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameDetailId}] was not found.");

            if (!gameDetail.SteamId.HasValue)
                throw new ValidationException($"The game with id [{gameDetailId}] has no steamId and can't be reloaded.");

            var achievements = steamApiGateway.GetAchievementByAppId(gameDetail.SteamId.Value);
            var percentages = steamApiGateway.GetAchievementPercentageByAppId(gameDetail.SteamId.Value);

            await Task.WhenAll(achievements, percentages);

            var achievementsResult = achievements.Result;
            var percentagesResult = percentages.Result;

            gameDetail.Achievements!.ForEach(a =>
            {
                a.Achieved = achievementsResult.FirstOrDefault(ac => ac.apiname == a.SteamName)?.achieved != 0;
                a.Percentage = percentagesResult.FirstOrDefault(p => p.name == a.SteamName)?.percent;
            });

            achievementsResult.RemoveAll(a => gameDetail.Achievements!.Select(a => a.SteamName).Contains(a.apiname));
            if (achievementsResult.Count != 0)
            {
                var achievementsEntity = achievementsResult.Select(a => a.ToEntity(percentagesResult.FirstOrDefault(p => p.name == a.apiname)?.percent)).ToList();
                achievementsEntity.ForEach(ae => ae.GameDetailId = gameDetail.Id);
                await achievementRepository.InsertRange(achievementsEntity);
            }

            await gameRepository.SaveChanges();
        }
    }
}
