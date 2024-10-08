namespace GameInterface.Models
{
    public class CreateSimpleGame
    {
        public GameToLoad? GameToLoad { get; set; }
        public required SimpleGame Game { get; set; }
    }
}
