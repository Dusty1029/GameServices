namespace GameService.API.PartyDtos
{
    public class PartyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFinish { get; set; }
        public int ActualRound { get; set; }

        public RoundDto? Round { get; set; }
        public List<TeamDto> Teams { get; set; } = [];
    }
}
