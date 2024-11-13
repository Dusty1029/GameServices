namespace Game.Dto.Games
{
    public class SimpleGameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Order { get; set; }

        public override string ToString() => Name;
    }
}
