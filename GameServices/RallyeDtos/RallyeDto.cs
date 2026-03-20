namespace GameService.API.RallyeDtos
{
    public class RallyeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RallyePlayerDto> Players { get; set; } = [];
        public List<SpecialDto> Specials { get; set; } = [];
    }
}
