namespace GameService.Infrastructure.Entities.Party
{
    public class PlayerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TeamEntity>? Teams { get; set; }
    }
}
