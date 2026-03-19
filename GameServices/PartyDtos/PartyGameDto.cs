namespace GameService.API.PartyDtos
{
    public class PartyGameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsTeamGame { get; set; }
    }
}
