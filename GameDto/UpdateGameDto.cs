
namespace Game.Dto
{
    public class UpdateGameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public IEnumerable<UpdateGameDetailDto>? GameDetails { get; set; }
    }
}
