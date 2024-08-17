using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using GameServices.API.Extensions.Entities;

namespace GameServices.API.BusinessLogics.Implementations
{
    public class AchievementBL : IAchievementBL
    {
        private readonly IAchievementRepository _achievementRepository;

        public AchievementBL(IAchievementRepository achievementRepository)
        {
            _achievementRepository = achievementRepository;
        }
        public async Task<Guid> CreateAchievement(AchievementDto achievementDto)
        {
            var achievementCreated = await _achievementRepository.InsertAndSave(achievementDto.ToEntity());

            return achievementCreated.Id;
        }

        public async Task DeleteAchievement(Guid achievementId)
        {
            var achievement = await _achievementRepository.Find(c => c.Id == achievementId);
            if (achievement is null)
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");

            await _achievementRepository.DeleteAndSave(achievement);
        }

        public async Task UpdateAchievedAchievement(Guid achievementId, bool achieved)
        {
            var achievement = await _achievementRepository.Find(c => c.Id == achievementId, noTracking: false);
            if (achievement is null)
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");

            achievement.Achieved = achieved;
            await _achievementRepository.SaveChanges();
        }

        public async Task UpdateAchievement(Guid achievementId, AchievementDto achievementDto)
        {
            var achievement = await _achievementRepository.Find(c => c.Id == achievementId, noTracking: false);
            if (achievement is null)
                throw new NotFoundException($"The achievement with id [{achievementId}] was not found.");

            achievementDto.ToEntity(achievement);
            await _achievementRepository.SaveChanges();
        }
    }
}
