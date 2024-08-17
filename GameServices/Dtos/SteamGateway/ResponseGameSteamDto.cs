namespace GameServices.API.Dtos.SteamGateway
{
    public class ResponseGameSteamDto
    {
        public int game_count { get; set; }
        public List<GameSteamDto> games { get; set; } = new List<GameSteamDto>();
    }
}
