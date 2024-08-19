using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos.PlaystationGateway;
using GameServices.API.Gateways.Interfaces;

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

        public async Task RefreshToken(string npsso)
        {
            var actualToken = await _parameterRepository.GetPlaystationTokenEntity();
            var token = await _playstationApiGateway.GetAuthenticationToken(npsso);
            if (token == null)
                throw new ValidationException($"The npsso [{npsso}] is invalid.");

            actualToken!.Value = token;
            await _parameterRepository.SaveChanges();
        }
    }
}
