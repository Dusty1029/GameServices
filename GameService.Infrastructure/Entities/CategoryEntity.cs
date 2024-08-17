
namespace GameService.Infrastructure.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSeed { get; set; }

        public List<GameEntity>? Games { get; set; }
    }
}
