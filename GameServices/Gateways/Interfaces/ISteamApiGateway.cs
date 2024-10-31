using GameService.API.Models.SteamGateway;

namespace GameService.API.Gateways.Interfaces
{
    public interface ISteamApiGateway
    {
        Task<List<GameSteam>> GetSteamGames(bool forceReload);
        Task<List<AchievementSteam>> GetAchievementByAppId(int appId);
        Task<List<AchievementPercentageSteam>> GetAchievementPercentageByAppId(int appId);
    }
}
