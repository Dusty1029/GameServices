namespace GameService.API.RallyeDtos
{
    public class SpecialDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<SpecialTimeDto> SpecialTimes { get; set; } = [];
    }
}