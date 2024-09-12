namespace GameService.API.Models.SteamGateway
{
    public class ResponseAchievementSteam
    {
        public string steamID { get; set; } = string.Empty;
        public string gameName { get; set; } = string.Empty;
        public List<AchievementSteam> achievements { get; set; } = [];
    }
}
