
namespace Game.Dto
{
    public class CreateGameDto
    {
        public string Name { get; set; } = string.Empty;
        public PlatformDto? Platform { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
    }
}
