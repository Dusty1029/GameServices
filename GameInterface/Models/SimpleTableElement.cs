namespace GameInterface.Models
{
    public class SimpleTableElement
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool CanBeDeleted { get; set; } = true;
    }
}
