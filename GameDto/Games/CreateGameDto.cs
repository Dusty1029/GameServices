using Game.Dto.Enums;
using Game.Dto.Series;

namespace Game.Dto.Games
{
    public class CreateGameDto
    {
        public string Name { get; set; } = string.Empty;
        public SimpleSerieDto? Serie { get; set; }
        public PlatformDto? Platform { get; set; }
        public GameDetailStatusEnumDto? Status { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
    }
}
