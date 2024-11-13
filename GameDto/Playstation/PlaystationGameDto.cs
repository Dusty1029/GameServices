using Game.Dto.Enums;

namespace Game.Dto.Playstation
{
    public class PlaystationGameDto
    {
        public string PlaystationId { get; set; } = string.Empty;
        public string TrophyTitleName { get; set; } = string.Empty;
        public PlatformEnumDto TrophyTitlePlatform { get; set; }
    }
}
