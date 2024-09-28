
namespace Game.Dto
{
    public class SearchGameItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Platforms { get; set; } = string.Empty;
        public string Categories { get; set; } = string.Empty;
        public string Serie {  get; set; } = string.Empty;
    }
}
