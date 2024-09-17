namespace GameService.Infrastructure.Entities
{
    public class AchievementEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SteamName { get; set; }
        public int? PlaystationTrophyId { get; set; }
        public string? Description { get; set; }
        public decimal? Percentage { get; set; }
        public bool Achieved { get; set; }
        public bool IsIgnored { get; set; }

        public Guid GameDetailId { get; set; }
        public GameDetailEntity? GameDetail { get; set; }
    }
}
