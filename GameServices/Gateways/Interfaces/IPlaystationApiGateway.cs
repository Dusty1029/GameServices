using GameService.Infrastructure.Entities.Enums;
using GameServices.API.Dtos.PlaystationGateway;

namespace GameServices.API.Gateways.Interfaces
{
    public interface IPlaystationApiGateway
    {
        Task<string?> GetAuthenticationToken(string npsso);
        Task<List<GamePlaystationDto>?> GetPlaystationGames(string token);
        Task<List<TrophyDto>> GetTrophiesByGame(string token, string gameId, PlatformEnumEntity platformEnum);
        Task<List<TrophyEarnedDto>> GetTrophyEarnedsByGame(string token, string gameId, PlatformEnumEntity platformEnum);
    }
}
