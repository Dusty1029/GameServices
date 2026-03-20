namespace GameService.API.RallyeDtos
{
    public class CreateSpecialTimeDto
    {
        public Guid SpecialId { get; set; }
        public Guid PlayerId { get; set; }
        public TimeSpan Time { get; set; }
    }
}
