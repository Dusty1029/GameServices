
namespace GameService.Infrastructure.Entities
{
    public class IgnoredGameEntity
    {
        public Guid Id { get; set; }
        public string? PlaystationId { get; set; }
        public int? SteamId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid PlatformId { get; set; }
        public PlatformEntity? Platform { get; set; }
    }
}
