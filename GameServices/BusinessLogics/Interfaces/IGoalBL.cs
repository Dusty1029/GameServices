using Game.Dto.Goal;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IGoalBL
    {
        Task<Guid> CreateGoal(Guid gameDetailId, CreateGoalDto createGoalDto);
        Task UpdateAchievedGoal(Guid goalId, bool achieved);
    }
}
