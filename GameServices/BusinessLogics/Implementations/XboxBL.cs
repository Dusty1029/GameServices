using CommonV2.Models.Exceptions;
using Game.Dto;
using Game.Dto.Enums;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Extensions.Entities.Enums;
using GameService.API.Gateways.Implementations;
using GameService.API.Gateways.Interfaces;
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

            var achievementsResult = xboxApiGateway.GetXboxAchievementsByGame(xboxGameDto.XboxGame.XboxId);
            var achievementsEarnedResult = xboxApiGateway.GetXboxAchievementsEarnedByGame(xboxGameDto.XboxGame.XboxId);

            await Task.WhenAll(platformIdResult, achievementsResult, achievementsEarnedResult);

            if (xboxGameDto.GameId.HasValue)
            {
                await gameDetailRepository.InsertAndSave(xboxGameDto.ToEntityWithGameId(achievementsResult.Result, achievementsEarnedResult.Result, platformIdResult.Result));
            }
            else
            {
                var gameEntity = xboxGameDto.ToEntity(achievementsResult.Result, achievementsEarnedResult.Result, platformIdResult.Result);
                if (gameEntity.SerieId is null)
                {
                    var defaultSerie = await serieRepository.FindDefaultSerie();
                    gameEntity.SerieId = defaultSerie.Id;
                }
                await gameRepository.CreateGame(gameEntity, categories);
                xboxGameDto.GameId = gameEntity.Id;
            }

            return xboxGameDto.GameId!.Value;
        }

        public async Task<List<XboxGameDto>> GetMissingXboxGames()
        {
            var xboxGameEntities = await gameDetailRepository.Get(g => g.XboxId != null, f => f.Include(g => g.Platform));
            var ignoredXboxGameResult = ignoredGameRepository.Get(g => g.XboxId != null, f => f.Include(gd => gd.Platform));
            var newXboxGamesResult = xboxApiGateway.GetXboxGames();

            await Task.WhenAll(ignoredXboxGameResult, newXboxGamesResult);

            var xboxGames = newXboxGamesResult.Result.Where(xg => xg.devices.Contains(PlatformEnumEntity.Xbox360.ToString()));

            var xboxGamesSplited = xboxGames?.SelectMany(
                    xg => xg.devices.Where(d => d == PlatformEnumEntity.Xbox360.ToString()).Select(d => new XboxGameDto
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

            var achievementsResult = xboxApiGateway.GetXboxAchievementsByGame(gameDetail.XboxId);
            var achievementsEarnedResult = xboxApiGateway.GetXboxAchievementsEarnedByGame(gameDetail.XboxId);

            await Task.WhenAll(achievementsResult, achievementsEarnedResult);

            var achievements = achievementsResult.Result;
            var achievementsEarned = achievementsEarnedResult.Result;

            gameDetail.Achievements!.ForEach(a =>
            {
                var achievement = achievements.First(ac => ac.id == a.XboxId);
                a.Achieved = achievementsEarned.Any(ae => ae.id == a.XboxId);
                a.Percentage = achievement.rarity?.currentPercentage;
            });

            achievements.RemoveAll(t => gameDetail.Achievements!.Select(a => a.XboxId).Contains(t.id));
            if (achievements.Count != 0)
            {
                var achievementEntities = achievements.Select(a => a.ToEntity(achievementsEarned.Any(ae => ae.id == a.id))).ToList();
                achievementEntities.ForEach(ae => ae.GameDetailId = gameDetail.Id);
                await achievementRepository.InsertRange(achievementEntities);
            }

            await gameRepository.SaveChanges();
        }
    }
}
