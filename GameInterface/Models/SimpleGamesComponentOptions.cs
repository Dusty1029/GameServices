using Game.Dto.Enums;

namespace GameInterface.Models
{
    public class SimpleGamesComponentOptions
    {
        public string Title { get; set; } = string.Empty;
        public bool HasColumnPlatform { get; set; } = true;
    }
}
