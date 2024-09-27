namespace GameInterface.Models
{
    public class CreateSimpleGame
    {
        public Guid? GameId { get; set; }
        public required SimpleGame Game { get; set; }
    }
}
