namespace GameService.API.Models.SteamGateway
{
    public class ResponseGameSteam
    {
        public int game_count { get; set; }
        public List<GameSteam> games { get; set; } = [];
    }
}
