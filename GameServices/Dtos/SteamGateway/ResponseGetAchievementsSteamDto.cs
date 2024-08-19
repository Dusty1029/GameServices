namespace GameServices.API.Dtos.SteamGateway
{
    public class ResponseGetAchievementsSteamDto
    {
        public ResponseAchievementSteamDto playerstats { get; set; } = new ResponseAchievementSteamDto();
    }
}
