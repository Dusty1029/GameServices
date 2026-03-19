namespace GameService.Infrastructure.Entities.Party
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsTeamGame { get; set; }
    }
}
