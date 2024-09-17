
namespace Game.Dto
{
    public class PlatformDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}
