using CommonV2.Models.Exceptions;
using Game.Dto.Goal;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.API.BusinessLogics.Implementations
{
    public class GoalBL(IGoalRepository goalRepository) : IGoalBL
    {
        public async Task<Guid> CreateGoal(Guid gameDetailId, CreateGoalDto createGoalDto)
        {
            var goalCreated = await goalRepository.InsertAndSave(createGoalDto.ToEntity());

            return goalCreated.Id;
        }

        public async Task UpdateAchievedGoal(Guid goalId, bool achieved)
        {
            var goal = await goalRepository.Find(g => g.Id == goalId, noTracking: false) ??
                throw new NotFoundException($"The goal with id [{goalId}] was not found.");

            goal.IsFulFilled = achieved;
            await goalRepository.SaveChanges();
        }
    }
}
