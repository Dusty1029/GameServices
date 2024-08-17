
namespace GameServices.API.Dtos.SteamGateway
{
    public class ResponseAchievementSteamDto
    {
        public string steamID { get; set; } = string.Empty;
        public string gameName { get; set; } = string.Empty;
        public List<AchievementSteamDto> achievements { get; set; } = new List<AchievementSteamDto>();
    }
}
