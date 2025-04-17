using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public GameDetailStatusEnumEntity GlobalStatus { get; set; }
        public int PlayOrder { get; set; }
        public string HowLongToBeatName { get; set; } = string.Empty;
        public decimal? MainStoryTime { get; set; } 
        public decimal? MainStoryAndExtraTime { get; set; } 
        public decimal? FullTime { get; set; } 

        public Guid? SerieId { get; set; }
        public SerieEntity? Serie { get; set; }
        public List<CategoryEntity>? Categories { get; set;}
        public List<GameDetailEntity>? GameDetails { get; set;}
    }
}
