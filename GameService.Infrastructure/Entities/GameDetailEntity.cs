
namespace GameService.Infrastructure.Entities
{
    public class GameDetailEntity
    {
        public Guid Id { get; set; }
        public int? SteamId { get; set; }
        public string? PlaystationId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsStarted { get; set; }

        public Guid PlatformId { get; set; }
        public PlatformEntity? Platform { get; set; }
        public Guid GameId { get; set; }
        public GameEntity? Game { get; set; }
        public List<AchievementEntity>? Achievements { get; set; }
        public List<GoalEntity>? Goals { get; set; }
    }
}
