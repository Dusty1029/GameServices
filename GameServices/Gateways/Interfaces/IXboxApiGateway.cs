using GameService.API.Models.XboxGateway;

namespace GameService.API.Gateways.Interfaces
{
    public interface IXboxApiGateway
    {
        public Task<List<XboxGame>> GetXboxGames();
        public Task<List<XboxAchievement>> GetXboxAchievementsByGame(string titleId);
        public Task<List<XboxAchievement>> GetXboxAchievementsEarnedByGame(string titleId);
    }
}
