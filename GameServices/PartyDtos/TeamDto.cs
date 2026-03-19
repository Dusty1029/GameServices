namespace GameService.API.PartyDtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PlayerDto>? Players { get; set; }
        public int? Points { get; set; }
    }
}
