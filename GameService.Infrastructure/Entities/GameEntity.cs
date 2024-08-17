using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? SteamId { get; set; }
        public bool IsIgnored { get; set; }
        public PlatformEnumEntity Platform { get; set; }

        public List<AchievementEntity>? Achievements { get; set;}
        public List<CategoryEntity>? Categories { get; set;}
    }
}
