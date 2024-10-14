
using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class GameDetailEntity
    {
        public Guid Id { get; set; }
        public int? SteamId { get; set; }
        public string? PlaystationId { get; set; }
        public string? XboxId { get; set; }
        public GameDetailStatusEnumEntity Status { get; set; }

        public Guid PlatformId { get; set; }
        public PlatformEntity? Platform { get; set; }
        public Guid GameId { get; set; }
        public GameEntity? Game { get; set; }
        public List<AchievementEntity>? Achievements { get; set; }
        public List<GoalEntity>? Goals { get; set; }
    }
}
