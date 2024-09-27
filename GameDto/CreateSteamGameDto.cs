namespace Game.Dto
{
    public class CreateSteamGameDto
    {
        public Guid? GameId { get; set; }
        public required SteamGameDto SteamGame { get; set; }
    }
}
