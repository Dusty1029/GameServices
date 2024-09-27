namespace Game.Dto
{
    public class CreatePlaystationGameDto
    {
        public Guid? GameId { get; set; }
        public required PlaystationGameDto PlaystationGame { get; set; }
    }
}
