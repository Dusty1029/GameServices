using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos.PlaystationGateway;
using GameServices.API.Extensions.Entities;
using GameServices.API.Gateways.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameServices.API.BusinessLogics.Implementations
{
    public class PlaystationBL : IPlaystationBL
    {
        private readonly IPlaystationApiGateway _playstationApiGateway;
        private readonly IParameterRepository _parameterRepository;
        private readonly IGameRepository _gameRepository;
        public PlaystationBL(IPlaystationApiGateway playstationApiGateway,
            IParameterRepository parameterRepository,
            IGameRepository gameRepository) 
        {
            _playstationApiGateway = playstationApiGateway;
            _parameterRepository = parameterRepository;
            _gameRepository = gameRepository;
        }

        public async Task<Guid> AddPlaystationGame(GamePlaystationDto gamePlaystationDto)
        {
            var actualToken = (await _parameterRepository.GetPlaystationToken()) ?? "";
            var platformEnum = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform);
            var trophiesResult = _playstationApiGateway.GetTrophiesByGame(actualToken, gamePlaystationDto.npCommunicationId, platformEnum);
            var trophiesEarnedResult = _playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gamePlaystationDto.npCommunicationId, platformEnum);

            await Task.WhenAll(trophiesResult, trophiesEarnedResult);

            var game = await _gameRepository.InsertAndSave(gamePlaystationDto.ToEntity(trophiesResult.Result, trophiesEarnedResult.Result));

            return game.Id;
        }

        public async Task<List<GamePlaystationDto>?> GetMissingPlaystationGames()
        {
            var actualToken = await _parameterRepository.GetPlaystationToken();
            var playstationGames = await _gameRepository.Get(g => g.PlaystationId != null);
            var newPlaystationGames = await _playstationApiGateway.GetPlaystationGames(actualToken ?? "");
            var playstationGamesSplited = newPlaystationGames?.SelectMany(
                pg => pg.trophyTitlePlatform.Split(',').Select(t => new GamePlaystationDto 
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

        public async Task<Guid> IgnorePlaystationGame(GamePlaystationDto gamePlaystationDto)
        {
            var gameEntity = gamePlaystationDto.ToEntity();
            gameEntity.IsIgnored = true;
            await _gameRepository.InsertAndSave(gameEntity);

            return gameEntity.Id;
        }

        public async Task RefreshToken(string npsso)
        {
            var actualToken = await _parameterRepository.GetPlaystationTokenEntity();
            var token = await _playstationApiGateway.GetAuthenticationToken(npsso);
            if (token == null)
                throw new ValidationException($"The npsso [{npsso}] is invalid.");

            actualToken!.Value = token;
            await _parameterRepository.SaveChanges();
        }

        public async Task ReloadPlaystationGame(Guid gameId)
        {
            var actualToken = (await _parameterRepository.GetPlaystationToken()) ?? "";
            var gameEntity = await _gameRepository.Find(g => g.Id == gameId, f => f.Include(g => g.Achievements), noTracking: false);
            if (gameEntity is null)
                throw new NotFoundException($"The game with id [{gameId}] was not found.");
            if (gameEntity.PlaystationId is null)
                throw new ValidationException($"The game with id [{gameId}] is not a Playstation game.");

            var trophiesEarned = await _playstationApiGateway.GetTrophyEarnedsByGame(actualToken, gameEntity.PlaystationId, gameEntity.Platform);

            gameEntity.Achievements!.ForEach(a =>
            {
                var trophy = trophiesEarned.First(t => t.trophyId == a.PlaystationTrophyId);
                a.Achieved = trophy.earned;
                a.Percentage = trophy.trophyEarnedRate;
            });

            await _gameRepository.SaveChanges();
        }
    }
}
