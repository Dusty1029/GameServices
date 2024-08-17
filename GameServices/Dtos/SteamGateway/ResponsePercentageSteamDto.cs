namespace GameServices.API.Dtos.SteamGateway
{
    public class ResponsePercentageSteamDto
    {
        public List<AchievementPercentageSteamDto> achievements { get; set; } = new List<AchievementPercentageSteamDto>();
    }
}
