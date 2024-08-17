using GameServices.API.Dtos.Enums;

namespace GameServices.API.Dtos
{
    public class SearchGameDto
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public string Name { get; set; } = string.Empty;
        public PlatformEnumDto? Platform { get; set; }
        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
    }
}
