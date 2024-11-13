using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.Extensions.Entities;

namespace GameService.API.BusinessLogics.Implementations
{
    public class AchievementBL(IAchievementRepository achievementRepository) : IAchievementBL
    {
        public async Task<Guid> CreateAchievement(AchievementDto achievementDto)
        {
            var achievementCreated = await achievementRepository.InsertAndSave(achievementDto.ToEntity());

            return achievementCreated.Id;
        }

        public async Task DeleteAchievement(Guid achievementId)
        {
            var achievement = await achievementRepository.Find(c => c.Id == achievementId) ??
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");
            await achievementRepository.DeleteAndSave(achievement);
        }

        public async Task UpdateAchievedAchievement(Guid achievementId, bool achieved)
        {
            var achievement = await achievementRepository.Find(c => c.Id == achievementId, noTracking: false) ??
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");
            achievement.Achieved = achieved;
            await achievementRepository.SaveChanges();
        }

        public async Task UpdateAchievement(Guid achievementId, AchievementDto achievementDto)
        {
            var achievement = await achievementRepository.Find(c => c.Id == achievementId, noTracking: false) ??
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");

            achievementDto.ToEntity(achievement);
            await achievementRepository.SaveChanges();
        }
    }
}
