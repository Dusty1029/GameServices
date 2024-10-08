using Game.Dto;

namespace GameInterface.Models
{
    public class GameToLoad
    {
        public SimpleGameDto? Game { get; set; }
        public SimpleSerieDto? Serie { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
    }
}
