namespace GameService.API.PartyDtos
{
    public class CreatePartyGameDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsTeamGame { get; set; }
    }
}
