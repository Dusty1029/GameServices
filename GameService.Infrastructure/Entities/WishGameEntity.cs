
namespace GameService.Infrastructure.Entities
{
    public class WishGameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid PlatformId { get; set; }
        public PlatformEntity? Platform { get; set; }
    }
}
