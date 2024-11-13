using Game.Dto.Series;

namespace Game.Dto.Playstation
{
    public class CreatePlaystationGameDto
    {
        public Guid? GameId { get; set; }
        public SimpleSerieDto? Serie { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public required PlaystationGameDto PlaystationGame { get; set; }
    }
}
