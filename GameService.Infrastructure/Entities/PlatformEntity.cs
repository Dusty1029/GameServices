using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class PlatformEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSeed { get; set; }
        public PlatformEnumEntity? PlatformEnum { get; set; }
        public List<GameDetailEntity>? GameDetails { get; set; }
        public List<WishGameEntity>? WishGames { get; set; }
        public List<IgnoredGameEntity>? IgnoredGames { get; set; }
    }
}
