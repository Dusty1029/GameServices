using Game.Dto.Goal;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IGoalService
    {
        Task<ApiResult<Guid>> CreateGoal(CancellationToken cancellationToken, Guid gameDetailId, CreateGoalDto createGoalDto);
        Task<ApiResult> UpdateAchievedGoal(CancellationToken cancellationToken, Guid goalId, bool achieved);
    }
}
