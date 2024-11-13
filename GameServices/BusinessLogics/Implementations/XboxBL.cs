using CommonV2.Models.Exceptions;
using Game.Dto.Enums;
using Game.Dto.Xbox;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Extensions.Entities.Enums;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.XboxGateway;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class XboxBL(IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IIgnoredGameRepository ignoredGameRepository,
        IPlatformRepository platformRepository,
        IAchievementRepository achievementRepository,
        ISerieRepository serieRepository,
        ICategoryRepository categoryRepository,
        IXboxApiGateway xboxApiGateway) : IXboxBL
    {
        public async Task<Guid> AddXboxGame(CreateXboxGameDto xboxGameDto)
        {
            if (xboxGameDto.GameId.HasValue)
            {
                var gameExist = await gameRepository.Exists(g => g.Id == xboxGameDto.GameId.Value);
                if (!gameExist)
                    throw new NotFoundException($"The game with id [{xboxGameDto.GameId}] was not found.");
            }

            if (xboxGameDto.Serie is not null && !await serieRepository.Exists(s => s.Id == xboxGameDto.Serie.Id))
                throw new NotFoundException($"The serie with id [{xboxGameDto.Serie!.Id}] was not found.");

            var categories = new List<CategoryEntity>();
            if (xboxGameDto.Categories is not null)
            {
                var categoryIds = xboxGameDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            var platformEnum = xboxGameDto.XboxGame.PlatformEnum.ToEntity();
            var platformIdResult = platformRepository.GetPlatformIdByEnum(platformEnum);

            var achievementsResult = xboxGameDto.XboxGame.PlatformEnum == PlatformEnumDto.Xbox360 ?
                xboxApiGateway.GetXbox360AchievementsByGame(xboxGameDto.XboxGame.XboxId) : xboxApiGateway.GetXboxAchievementsByGame(xboxGameDto.XboxGame.XboxId);

            var achievementsEarnedResult = xboxGameDto.XboxGame.PlatformEnum == PlatformEnumDto.Xbox360 ?
                xboxApiGateway.GetXbox360AchievementsEarnedByGame(xboxGameDto.XboxGame.XboxId) : Task.FromResult((List<XboxAchievement>?)null);

            await Task.WhenAll(platformIdResult, achievementsResult, achievementsEarnedResult);

            if (xboxGameDto.GameId.HasValue)
            {
                await gameDetailRepository.InsertAndSave(xboxGameDto.ToEntityWithGameId(achievementsResult.Result, platformIdResult.Result, achievementsEarnedResult.Result));
            }
            else
            {
                var gameEntity = xboxGameDto.ToEntity(achievementsResult.Result, platformIdResult.Result, achievementsEarnedResult.Result);
                if (gameEntity.SerieId is null)
                {
                    var defaultSerie = await serieRepository.FindDefaultSerie();
                    gameEntity.SerieId = defaultSerie.Id;
                }
                gameEntity.PlayOrder = await gameRepository.NextPlayOrderBySerie(gameEntity.SerieId.Value);

                await gameRepository.CreateGame(gameEntity, categories);
                xboxGameDto.GameId = gameEntity.Id;
            }

            return xboxGameDto.GameId!.Value;
        }

        public async Task<List<XboxGameDto>> GetMissingXboxGames(bool forceReload)
        {
            var xboxGameEntities = await gameDetailRepository.Get(g => g.XboxId != null, f => f.Include(g => g.Platform));
            var ignoredXboxGameResult = ignoredGameRepository.Get(g => g.XboxId != null, f => f.Include(gd => gd.Platform));
            var newXboxGamesResult = xboxApiGateway.GetXboxGames(forceReload);

            await Task.WhenAll(ignoredXboxGameResult, newXboxGamesResult);

            var xboxPlatforms = PlatformEnumExtensions.XboxPlatformEnums.Select(p => p.ToString());

            var xboxGames = newXboxGamesResult.Result.Where(xg => xboxPlatforms.Any(xp => xg.devices.Contains(xp)));

            var xboxGamesSplited = xboxGames?.SelectMany(
                    xg => xg.devices.Where(d => xboxPlatforms.Any(xp => xp == d)).Select(d => new XboxGameDto
                    {
                        XboxId = xg.titleId,
                        Name = xg.name,
                        PlatformEnum = Enum.Parse<PlatformEnumDto>(d)
                    }
                )).ToList();

            xboxGamesSplited?.RemoveAll(pgs => ignoredXboxGameResult.Result.Any(
                pg => pg.XboxId == pgs.XboxId && pg.Platform!.PlatformEnum == pgs.PlatformEnum.ToEntity()
            ));
            xboxGamesSplited?.RemoveAll(pgs => xboxGameEntities.Any(
                pg => pg.XboxId == pgs.XboxId && pg.Platform!.PlatformEnum == pgs.PlatformEnum.ToEntity()
            ));

            return xboxGamesSplited?.OrderBy(pg => pg.Name).ToList() ?? [];
        }

        public async Task IgnoreXboxGame(XboxGameDto xboxGameDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var platformEnum = xboxGameDto.PlatformEnum.ToEntity();
                var platformIdResult = await platformRepository.GetPlatformIdByEnum(platformEnum);

                var ignoredGameEntity = xboxGameDto.ToEntity();
                ignoredGameEntity.PlatformId = platformIdResult;

                await ignoredGameRepository.InsertAndSave(ignoredGameEntity);
            }
            else
            {
                var ignoredGameEntity = await ignoredGameRepository.Find(ig => ig.XboxId == xboxGameDto.XboxId) ??
                    throw new NotFoundException($"The game with playstation id [{xboxGameDto.XboxId}] was not found.");

                await ignoredGameRepository.DeleteAndSave(ignoredGameEntity);
            }
        }

        public async Task ReloadXboxGame(Guid gameDetailId)
        {
            var gameDetail = await gameDetailRepository.Find(g => g.Id == gameDetailId,
                f => f.Include(g => g.Achievements).Include(g => g.Platform), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameDetailId}] was not found.");

            if (gameDetail.XboxId is null)
                throw new ValidationException($"The game with id [{gameDetailId}] is not a Playstation game.");

            var achievementsResult = gameDetail.Platform!.PlatformEnum == PlatformEnumEntity.Xbox360 ? 
                xboxApiGateway.GetXbox360AchievementsByGame(gameDetail.XboxId) : xboxApiGateway.GetXboxAchievementsByGame(gameDetail.XboxId);

            var achievementsEarnedResult = gameDetail.Platform!.PlatformEnum == PlatformEnumEntity.Xbox360 ?
                xboxApiGateway.GetXbox360AchievementsEarnedByGame(gameDetail.XboxId) : Task.FromResult((List<XboxAchievement>?)null);

            await Task.WhenAll(achievementsResult, achievementsEarnedResult!);

            var achievements = achievementsResult.Result;
            var achievementsEarned = achievementsEarnedResult?.Result;

            gameDetail.Achievements!.ForEach(a =>
            {
                var achievement = achievements.First(ac => ac.id == a.XboxId);
                a.Achieved = achievementsEarned is not null ? achievementsEarned.Any(ae => ae.id == a.XboxId) : achievement.progressState == XboxGameExtensions.AchievedProgressState;
                a.Percentage = achievement.rarity?.currentPercentage;
            });

            achievements.RemoveAll(t => gameDetail.Achievements!.Select(a => a.XboxId).Contains(t.id));
            if (achievements.Count != 0)
            {
                var achievementEntities = achievements.Select(a => a.ToEntity(achievementsEarned is not null ? achievementsEarned.Any(ae => ae.id == a.id) : a.progressState == XboxGameExtensions.AchievedProgressState)).ToList();
                achievementEntities.ForEach(ae => ae.GameDetailId = gameDetail.Id);
                await achievementRepository.InsertRange(achievementEntities);
            }

            await gameRepository.SaveChanges();
        }
    }
}
