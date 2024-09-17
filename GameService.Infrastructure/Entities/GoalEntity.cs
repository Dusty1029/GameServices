
namespace GameService.Infrastructure.Entities
{
    public class GoalEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsFulFilled { get; set; }

        public Guid GameDetailId { get; set; }
        public GameDetailEntity? GameDetail { get; set; }
    }
}
