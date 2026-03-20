namespace GameService.API.RallyeDtos
{
    public class SpecialTimeDto
    {
        public Guid Id { get; set; }
        public RallyePlayerDto Player { get; set; }
        public TimeSpan Time { get; set; }
    }
}