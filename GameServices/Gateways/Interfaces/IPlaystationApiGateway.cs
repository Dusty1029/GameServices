using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.Gateways.Interfaces
{
    public interface IPlaystationApiGateway
    {
        Task<List<GamePlaystation>> GetPlaystationGames(string token, bool forceReload);
        Task<List<Trophy>> GetTrophiesByGame(string token, string gameId, PlatformEnumEntity platformEnum);
        Task<List<TrophyEarned>> GetTrophyEarnedsByGame(string token, string gameId, PlatformEnumEntity platformEnum);
    }
}
