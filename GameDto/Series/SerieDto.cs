using Game.Dto.Games;

namespace Game.Dto.Series
{
    public class SerieDto
    {
        public Guid Id { get; set; }
        public string Serie { get; set; } = string.Empty;
        public List<SimpleSerieDto> ChildSeries { get; set; } = [];
        public SimpleSerieDto? ParentSerie { get; set; }
        public List<SimpleGameDto> Games { get; set; } = [];
    }
}
