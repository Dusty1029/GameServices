
namespace GameService.Infrastructure.Entities
{
    public class SerieEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid? ParentSerieId { get; set; }
        public SerieEntity? ParentSerie { get; set; }
        public List<SerieEntity>? ChildrenSeries { get; set; }
        public List<GameEntity>? Games { get; set; }

    }
}
