using Game.Dto.Enums;

namespace Game.Dto
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PlatformEnumDto Platform { get; set; }
        public List<CategoryDto>? Categories { get; set; }
        public List<AchievementDto>? Achievements { get; set;}
    }
}
