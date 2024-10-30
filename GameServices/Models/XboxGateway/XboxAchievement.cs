namespace GameService.API.Models.XboxGateway
{
    public class XboxAchievement
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description {  get; set; } = string.Empty;
        public string? progressState { get; set; }
        public XboxAchievementRarity? rarity { get; set; }
    }
}
