namespace Game.Dto
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required IEnumerable<CategoryDto> Categories { get; set; }
        public required IEnumerable<GameDetailDto> GameDetails { get; set; }
    }
}
