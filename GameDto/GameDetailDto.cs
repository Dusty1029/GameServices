
using Game.Dto.Enums;

namespace Game.Dto
{
    public class GameDetailDto
    {
        public Guid Id { get; set; }
        public required PlatformDto Platform { get; set; }
        public required IEnumerable<AchievementDto> Achievements { get; set; }
        public decimal? AchievementCompletion {  get; set; }
        public GameDetailStatusEnumDto Status { get; set; }

        public string PlatformAndPercentage => $"{Platform.Name}" + (AchievementCompletion.HasValue ? $" - {AchievementCompletion}%" : "");
    }
}
