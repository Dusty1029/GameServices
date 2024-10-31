using GameService.API.Models.XboxGateway;

namespace GameService.API.Gateways.Interfaces
{
    public interface IXboxApiGateway
    {
        public Task<List<XboxGame>> GetXboxGames(bool forceReload);
        public Task<List<XboxAchievement>> GetXbox360AchievementsByGame(string titleId);
        public Task<List<XboxAchievement>?> GetXbox360AchievementsEarnedByGame(string titleId);
        public Task<List<XboxAchievement>> GetXboxAchievementsByGame(string titleId);
    }
}
