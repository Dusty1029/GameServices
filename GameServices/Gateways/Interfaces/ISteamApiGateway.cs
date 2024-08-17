using GameServices.API.Dtos.SteamGateway;

namespace GameServices.API.Gateways.Interfaces
{
    public interface ISteamApiGateway
    {
        Task<List<GameSteamDto>?> GetSteamGames();
        Task<List<AchievementSteamDto>> GetAchievementByAppId(int appId);
        Task<List<AchievementPercentageSteamDto>> GetAchievementPercentageByAppId(int appId);
    }
}
