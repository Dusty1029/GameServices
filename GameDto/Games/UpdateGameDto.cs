using Game.Dto.Series;

namespace Game.Dto.Games
{
    public class UpdateGameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SimpleSerieDto? Serie { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public IEnumerable<UpdateGameDetailDto>? GameDetails { get; set; }
    }
}
