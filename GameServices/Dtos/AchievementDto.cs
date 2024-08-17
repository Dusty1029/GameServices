
namespace GameServices.API.Dtos
{
    public class AchievementDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Percentage { get; set; }
        public bool Achievement { get; set; }
        public Guid GameId { get; set; }
    }
}
