namespace Game.Dto
{
    public class CreateSerieDto
    {
        public string Serie { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public List<SimpleGameDto> Games { get; set; } = [];
    }
}
