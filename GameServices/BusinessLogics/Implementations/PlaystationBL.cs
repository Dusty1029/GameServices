using CommonV2.Models.Exceptions;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;
using Game.Dto;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PlaystationBL(IPlaystationApiGateway playstationApiGateway,
        IParameterRepository parameterRepository,
        IGameRepository gameRepository,
        IGameDetailRepository gameDetailRepository,
        IPlatformRepository platformRepository,
        IIgnoredGameRepository ignoredGameRepository,
        IAchievementRepository achievementRepository) : IPlaystationBL
    {
        public async Task<Guid> AddPlaystationGame(PlaystationGameDto gamePlaystationDto)
        {
            var actualToken = (await parameterRepository.GetPlaystationToken()) ?? "";
            var platformEnum = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.TrophyTitlePlatform);
            var platformIdResult = GetPlaystationPlatformId(platformEnum);

            var trophiesResult = playstationApiGateway.GetTrophiesByGame(actualToken, gamePlaystationDto.PlaystationId, platformEnum);
            var trophiesEarnedResult = playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gamePlaystationDto.PlaystationId, platformEnum);

            await Task.WhenAll(platformIdResult, trophiesResult, trophiesEarnedResult);

            var game = await gameRepository.InsertAndSave(gamePlaystationDto.ToEntity(trophiesResult.Result, trophiesEarnedResult.Result, platformIdResult.Result));

            return game.Id;
        }

        public async Task IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored)
        {
            if (isIgnored)
            {
                var platformEnum = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.TrophyTitlePlatform);
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
            var actualToken = (await parameterRepository.GetPlaystationToken()) ?? "";

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

        public async Task RefreshToken(string npsso)
        {
            var actualToken = await parameterRepository.GetPlaystationTokenEntity();
            var token = await playstationApiGateway.GetAuthenticationToken(npsso);
            if (token == null)
                throw new ValidationException($"The npsso [{npsso}] is invalid.");

            actualToken!.Value = token;
            await parameterRepository.SaveChanges();
        }

        public async Task<List<PlaystationGameDto>> GetMissingPlaystationGames()
        {
            var actualToken = await parameterRepository.GetPlaystationToken();
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
                pg => pg.PlaystationId == pgs.PlaystationId && pg.Platform!.PlatformEnum.ToString() == pgs.TrophyTitlePlatform
            ));
            playstationGamesSplited?.RemoveAll(pgs => playstationGames.Any(
                pg => pg.PlaystationId == pgs.PlaystationId && pg.Platform!.PlatformEnum.ToString() == pgs.TrophyTitlePlatform
            ));

            return playstationGamesSplited?.OrderBy(pg => pg.TrophyTitleName).ToList() ?? [];
        }

        private Task<Guid> GetPlaystationPlatformId(PlatformEnumEntity platformEnum) => platformRepository.FindSelect(p => p.PlatformEnum == platformEnum, f => f.Select(p => p.Id));
    }
}
