namespace GameService.API.PartyDtos
{
    public class SimplePartyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFinish { get; set; }
    }
}
