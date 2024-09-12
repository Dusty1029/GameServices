using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IAchievementBL
    {
        Task<Guid> CreateAchievement(AchievementDto achievementDto);
        Task DeleteAchievement(Guid achievementId);
        Task UpdateAchievedAchievement(Guid achievementId, bool achieved);
        Task UpdateAchievement(Guid achievementId, AchievementDto achievementDto);
    }
}
