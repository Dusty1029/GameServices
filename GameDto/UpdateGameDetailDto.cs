namespace Game.Dto
{
    public class UpdateGameDetailDto
    {
        public Guid Id { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
    }
}
