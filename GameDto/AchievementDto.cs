
namespace Game.Dto
{
    public class AchievementDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Percentage { get; set; }
        public bool Achieved { get; set; }

        public string PercentageAndName => $"{Name}" + (Percentage.HasValue ? $" {Percentage}%" : "");
    }
}
