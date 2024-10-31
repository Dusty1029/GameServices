using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Entities;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SteamBL(IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IIgnoredGameRepository ignoredGameRepository,
        IPlatformRepository platformRepository,
        IAchievementRepository achievementRepository,
        ISerieRepository serieRepository,
        ICategoryRepository categoryRepository,
        ISteamApiGateway steamApiGateway) : ISteamBL
    {
        public async Task<Guid> AddSteamGame(CreateSteamGameDto gameSteamDto)
        { 
            if (gameSteamDto.GameId.HasValue)
            {
                var gameExist = await gameRepository.Exists(g => g.Id == gameSteamDto.GameId.Value);
                if (!gameExist)
                    throw new NotFoundException($"The game with id [{gameSteamDto.GameId}] was not found.");
            }

            if (gameSteamDto.Serie is not null && !await serieRepository.Exists(s => s.Id == gameSteamDto.Serie.Id))
                throw new NotFoundException($"The serie with id [{gameSteamDto.Serie!.Id}] was not found.");

            var categories = new List<CategoryEntity>();
            if (gameSteamDto.Categories is not null)
            {
                var categoryIds = gameSteamDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            var steamPlatformId = GetSteamPlatformId();
            var achievementsResult = steamApiGateway.GetAchievementByAppId(gameSteamDto.SteamGame.SteamId);
            var percentagesResult = steamApiGateway.GetAchievementPercentageByAppId(gameSteamDto.SteamGame.SteamId);

            await Task.WhenAll(achievementsResult, percentagesResult, steamPlatformId);

            if (gameSteamDto.GameId.HasValue)
            {
                await gameDetailRepository.InsertAndSave(gameSteamDto.ToEntityWithGameId(achievementsResult.Result, percentagesResult.Result, steamPlatformId.Result));
            }
            else
            {
                var gameEntity = gameSteamDto.ToEntity(achievementsResult.Result, percentagesResult.Result, steamPlatformId.Result);
                if (gameEntity.SerieId is null)
                {
                    var defaultSerie = await serieRepository.FindDefaultSerie();
                    gameEntity.SerieId = defaultSerie.Id;
                }
                gameEntity.PlayOrder = await gameRepository.NextPlayOrderBySerie(gameEntity.SerieId.Value);

                await gameRepository.CreateGame(gameEntity, categories);
                gameSteamDto.GameId = gameEntity.Id;
            }           

            return gameSteamDto.GameId!.Value;
        }

        public async Task<List<SteamGameDto>> GetMissingSteamGames(bool forceReload)
        {
            var steamIds = await gameDetailRepository.GetSelect(f => f.Select(g => g.SteamId), g => g.SteamId != null);
            var ignoredSteamIds = ignoredGameRepository.GetSelect(i => i.Select(i => i.SteamId));
            var steamGames = steamApiGateway.GetSteamGames(forceReload);

            await Task.WhenAll(ignoredSteamIds, steamGames);

            return steamGames.Result.Where(sg => !steamIds.Union(ignoredSteamIds.Result).Contains(sg.appid)).OrderBy(sg => sg.name).Select(sg => sg.ToDto()).ToList() ?? [];
        }

        public async Task IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var steamPlatformId = await GetSteamPlatformId();
                var ignoredSteamGame = gameSteamDto.ToEntity();
                ignoredSteamGame.PlatformId = steamPlatformId;
                await ignoredGameRepository.InsertAndSave(ignoredSteamGame);
            }
            else
            {
                var ignoredGameEntity = await ignoredGameRepository.Find(ig => ig.SteamId == gameSteamDto.SteamId) ??
                    throw new NotFoundException($"The game with steam id [{gameSteamDto.SteamId}] was not found.");

                await ignoredGameRepository.DeleteAndSave(ignoredGameEntity);
            }
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

        private Task<Guid> GetSteamPlatformId() => platformRepository.FindSelect(p => p.PlatformEnum == PlatformEnumEntity.Steam, f => f.Select(p => p.Id));
    }
}
