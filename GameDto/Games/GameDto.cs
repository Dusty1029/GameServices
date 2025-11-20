using Game.Dto.Series;

namespace Game.Dto.Games
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string HowLongToBeatName {  get; set; }
        public decimal? MainStoryTime { get; set; }
        public decimal? MainStoryAndExtraTime { get; set; }
        public decimal? FullTime { get; set; }
        public SimpleSerieDto? Serie { get; set; }
        public required IEnumerable<CategoryDto> Categories { get; set; }
        public required IEnumerable<GameDetailDto> GameDetails { get; set; }
    }
}
