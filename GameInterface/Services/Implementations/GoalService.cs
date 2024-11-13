using Game.Dto.Goal;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class GoalService(IGenericService genericService) : IGoalService
    {
        private readonly string beginPath = "goal";
        public Task<ApiResult<Guid>> CreateGoal(CancellationToken cancellationToken, Guid gameDetailId, CreateGoalDto createGoalDto)
            => genericService.PostResult<Guid>(cancellationToken, createGoalDto, path: $"{beginPath}/gameDetail/{gameDetailId}");

        public Task<ApiResult> UpdateAchievedGoal(CancellationToken cancellationToken, Guid goalId, bool achieved)
            => genericService.PutResult(cancellationToken, path: $"{beginPath}/{goalId}/achieved/{achieved}");
    }
}
