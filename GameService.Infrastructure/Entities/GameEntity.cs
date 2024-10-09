using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusOrder { get; set; }

        public Guid? SerieId { get; set; }
        public SerieEntity? Serie { get; set; }
        public List<CategoryEntity>? Categories { get; set;}
        public List<GameDetailEntity>? GameDetails { get; set;}
    }
}
