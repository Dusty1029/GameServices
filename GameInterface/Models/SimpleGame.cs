namespace GameInterface.Models
{
    public class SimpleGame
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Platform { get; set; }
    }
}
