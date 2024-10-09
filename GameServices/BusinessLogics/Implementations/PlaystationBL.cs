using CommonV2.Models.Exceptions;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;
using Game.Dto;
using GameService.API.Extensions.Entities.Enums;
using GameService.Infrastructure.Entities;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PlaystationBL(IPlaystationApiGateway playstationApiGateway,
        IParameterRepository parameterRepository,
        IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IPlatformRepository platformRepository,
        IIgnoredGameRepository ignoredGameRepository,
        ISerieRepository serieRepository,
        ICategoryRepository categoryRepository,
        IAchievementRepository achievementRepository) : IPlaystationBL
    {
        public async Task<Guid> AddPlaystationGame(CreatePlaystationGameDto gamePlaystationDto)
        {
            if (gamePlaystationDto.GameId.HasValue)
            {
                var gameExist = await gameRepository.Exists(g => g.Id == gamePlaystationDto.GameId.Value);
                if (!gameExist)
                    throw new NotFoundException($"The game with id [{gamePlaystationDto.GameId}] was not found.");
            }

            if (gamePlaystationDto.Serie is not null && !await serieRepository.Exists(s => s.Id == gamePlaystationDto.Serie.Id))
                throw new NotFoundException($"The serie with id [{gamePlaystationDto.Serie!.Id}] was not found.");

            var categories = new List<CategoryEntity>();
            if (gamePlaystationDto.Categories is not null)
            {
                var categoryIds = gamePlaystationDto.Categories.Select(c => c.Id);
                categories = await categoryRepository.Get(c => categoryIds.Contains(c.Id));
                var missingCategories = categoryIds.Except(categories.Select(c => c.Id));
                if (missingCategories.Any())
                    throw new NotFoundException($"The categories with id [{string.Join(", ", missingCategories)}] was not found.");
            }

            var actualToken = await RefreshToken();
            var platformEnum = gamePlaystationDto.PlaystationGame.TrophyTitlePlatform.ToEntity();
            var platformIdResult = GetPlaystationPlatformId(platformEnum);

            var trophiesResult = playstationApiGateway.GetTrophiesByGame(actualToken, gamePlaystationDto.PlaystationGame.PlaystationId, platformEnum);
            var trophiesEarnedResult = playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gamePlaystationDto.PlaystationGame.PlaystationId, platformEnum);

            await Task.WhenAll(platformIdResult, trophiesResult, trophiesEarnedResult);

            if(gamePlaystationDto.GameId.HasValue)
            {
                await gameDetailRepository.InsertAndSave(gamePlaystationDto.ToEntityWithGameId(trophiesResult.Result, trophiesEarnedResult.Result, platformIdResult.Result));
            }
            else
            {
                var gameEntity = gamePlaystationDto.ToEntity(trophiesResult.Result, trophiesEarnedResult.Result, platformIdResult.Result);
                if (gameEntity.SerieId is null)
                {
                    var defaultSerie = await serieRepository.FindDefaultSerie();
                    gameEntity.SerieId = defaultSerie.Id;
                }
                await gameRepository.CreateGame(gameEntity, categories);
                gamePlaystationDto.GameId = gameEntity.Id;
            }

            return gamePlaystationDto.GameId!.Value;
        }

        public async Task IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var platformEnum = gamePlaystationDto.TrophyTitlePlatform.ToEntity();
                var platformIdResult = await GetPlaystationPlatformId(platformEnum);

                var ignoredGameEntity = gamePlaystationDto.ToEntity();
                ignoredGameEntity.PlatformId = platformIdResult;

                await ignoredGameRepository.InsertAndSave(ignoredGameEntity);
            }
            else
            {
                var ignoredGameEntity = await ignoredGameRepository.Find(ig => ig.PlaystationId == gamePlaystationDto.PlaystationId) ??
                    throw new NotFoundException($"The game with playstation id [{gamePlaystationDto.PlaystationId}] was not found.");

                await ignoredGameRepository.DeleteAndSave(ignoredGameEntity);
            }
        }

        public async Task ReloadPlaystationGame(Guid gameDetailId)
        {
            var actualToken = await RefreshToken();

            var gameDetail = await gameDetailRepository.Find(g => g.Id == gameDetailId,
                f => f.Include(g => g.Achievements).Include(g => g.Platform), noTracking: false) ??
                throw new NotFoundException($"The game with id [{gameDetailId}] was not found.");

            if (gameDetail.PlaystationId is null)
                throw new ValidationException($"The game with id [{gameDetailId}] is not a Playstation game.");


            var trophiesResult = playstationApiGateway.GetTrophiesByGame(actualToken, gameDetail.PlaystationId, gameDetail.Platform!.PlatformEnum!.Value);
            var trophiesEarnedResult = playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gameDetail.PlaystationId, gameDetail.Platform!.PlatformEnum!.Value);

            await Task.WhenAll(trophiesResult, trophiesEarnedResult);

            var trophies = trophiesResult.Result;
            var trophiesEarned = trophiesEarnedResult.Result;

            gameDetail.Achievements!.ForEach(a =>
            {
                var trophy = trophiesEarned.First(t => t.trophyId == a.PlaystationTrophyId);
                a.Achieved = trophy.earned;
                a.Percentage = trophy.trophyEarnedRate;
            });

            trophies.RemoveAll(t => gameDetail.Achievements!.Select(a => a.PlaystationTrophyId).Contains(t.trophyId));
            if (trophies.Count != 0)
            {
                var trophyEntities = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList();
                trophyEntities.ForEach(ae => ae.GameDetailId = gameDetail.Id);
                await achievementRepository.InsertRange(trophyEntities);
            }

            await gameRepository.SaveChanges();
        }

        public async Task<string> RefreshToken(string npsso)
        {
            var npssoEntity = await parameterRepository.GetNpssoEntity();
            npssoEntity!.Value = npsso;

            return await RefreshToken(npssoEntity);
        }

        private async Task<string> RefreshToken(ParameterEntity? npssoEntity = null)
        {
            var actualToken = await parameterRepository.GetPlaystationTokenEntity();
            npssoEntity ??= await parameterRepository.GetNpssoEntity();

            var token = await playstationApiGateway.GetAuthenticationToken(npssoEntity!.Value) ??
                throw new CommonV2.Models.Exceptions.UnauthorizedAccessException($"The npsso [{npssoEntity!.Value}] is invalid.");

            actualToken!.Value = token;
            await parameterRepository.SaveChanges();

            return token;
        }

        public async Task<List<PlaystationGameDto>> GetMissingPlaystationGames()
        {
            var actualToken = await RefreshToken();
            var playstationGames = await gameDetailRepository.Get(g => g.PlaystationId != null, f => f.Include(g => g.Platform));
            var ignoredPlaystationGameResult = ignoredGameRepository.Get(g => g.PlaystationId != null, f => f.Include(gd => gd.Platform));
            var newPlaystationGamesResult = playstationApiGateway.GetPlaystationGames(actualToken ?? "");

            await Task.WhenAll(ignoredPlaystationGameResult, newPlaystationGamesResult);

            var playstationGamesSplited = newPlaystationGamesResult.Result?.SelectMany(
                    pg => pg.trophyTitlePlatform.Split(',').Select(t => new GamePlaystation
                    {
                        trophyTitlePlatform = t,
                        npCommunicationId = pg.npCommunicationId,
                        trophyTitleName = pg.trophyTitleName
                    }.ToDto()
                )).ToList();

            playstationGamesSplited?.RemoveAll(pgs => ignoredPlaystationGameResult.Result.Any(
                pg => pg.PlaystationId == pgs.PlaystationId && pg.Platform!.PlatformEnum == pgs.TrophyTitlePlatform.ToEntity()
            ));
            playstationGamesSplited?.RemoveAll(pgs => playstationGames.Any(
                pg => pg.PlaystationId == pgs.PlaystationId && pg.Platform!.PlatformEnum == pgs.TrophyTitlePlatform.ToEntity()
            ));

            return playstationGamesSplited?.OrderBy(pg => pg.TrophyTitleName).ToList() ?? [];
        }

        private Task<Guid> GetPlaystationPlatformId(PlatformEnumEntity platformEnum) => platformRepository.FindSelect(p => p.PlatformEnum == platformEnum, f => f.Select(p => p.Id));
    }
}
