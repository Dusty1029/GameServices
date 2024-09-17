namespace Game.Dto
{
    public class SearchGameDto
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? PlatformId { get; set; }
        public List<Guid>? CategoriesId { get; set; }
    }
}
