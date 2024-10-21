
using Game.Dto.Enums;

namespace Game.Dto
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
