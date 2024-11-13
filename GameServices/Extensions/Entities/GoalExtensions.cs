using Game.Dto.Goal;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class GoalExtensions
    {
        public static GoalEntity ToEntity(this CreateGoalDto goalDto) => new()
        {
            GameDetailId = goalDto.GameDetailId,
            Title = goalDto.Name,
            IsFulFilled = false
        };

        public static GoalDto ToDto(this GoalEntity goalEntity) => new() 
        {
            Id = goalEntity.Id,
            Name = goalEntity.Title,
            Achieved = goalEntity.IsFulFilled
        };
    }
}
