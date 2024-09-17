using CommonV2.Models.Exceptions;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PlaystationBL(IPlaystationApiGateway playstationApiGateway,
        IParameterRepository parameterRepository,
        IGameRepository gameRepository) : IPlaystationBL
    {
        /*public async Task<Guid> AddPlaystationGame(GamePlaystation gamePlaystationDto)
        {
            var actualToken = (await parameterRepository.GetPlaystationToken()) ?? "";
            var platformEnum = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform);
            var trophiesResult = playstationApiGateway.GetTrophiesByGame(actualToken, gamePlaystationDto.npCommunicationId, platformEnum);
            var trophiesEarnedResult = playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gamePlaystationDto.npCommunicationId, platformEnum);

            await Task.WhenAll(trophiesResult, trophiesEarnedResult);

            var game = await gameRepository.InsertAndSave(gamePlaystationDto.ToEntity(trophiesResult.Result, trophiesEarnedResult.Result));

            return game.Id;
        }

        public async Task<List<GamePlaystation>?> GetMissingPlaystationGames()
        {
            var actualToken = await parameterRepository.GetPlaystationToken();
            var playstationGames = await gameRepository.Get(g => g.PlaystationId != null);
            var newPlaystationGames = await playstationApiGateway.GetPlaystationGames(actualToken ?? "");
            var playstationGamesSplited = newPlaystationGames?.SelectMany(
                pg => pg.trophyTitlePlatform.Split(',').Select(t => new GamePlaystation 
                    { 
                        trophyTitlePlatform = t,
                        npCommunicationId = pg.npCommunicationId,
                        trophyTitleName = pg.trophyTitleName
                    }
                ));

            return playstationGamesSplited?.Where(pgs => !playstationGames.Any(
                        pg => pg.PlaystationId == pgs.npCommunicationId && pg.Platform == Enum.Parse<PlatformEnumEntity>(pgs.trophyTitlePlatform)
                    )).OrderBy(pg => pg.trophyTitleName).ToList();
        }

        public async Task<Guid> IgnorePlaystationGame(GamePlaystation gamePlaystationDto)
        {
            var gameEntity = gamePlaystationDto.ToEntity();
            gameEntity.IsIgnored = true;
            await gameRepository.InsertAndSave(gameEntity);

            return gameEntity.Id;
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

        public async Task ReloadPlaystationGame(Guid gameId)
        {
            var actualToken = (await parameterRepository.GetPlaystationToken()) ?? "";
            var gameEntity = await gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Achievements), noTracking: false);
            if (gameEntity is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");
            if (gameEntity.PlaystationId is null)
                throw new ValidationException($"The game with id [{gameId}] is not a Playstation game.");

            var trophiesEarned = await playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gameEntity.PlaystationId, gameEntity.Platform);

            gameEntity.Achievements!.ForEach(a =>
            {
                var trophy = trophiesEarned.First(t => t.trophyId == a.PlaystationTrophyId);
                a.Achieved = trophy.earned;
                a.Percentage = trophy.trophyEarnedRate;
            });

            await gameRepository.SaveChanges();
        }*/
    }
}
