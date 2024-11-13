using Game.Dto.Games;

namespace Game.Dto.Series
{
    public class CreateSerieDto
    {
        public string Serie { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public List<SimpleGameDto> Games { get; set; } = [];
    }
}
