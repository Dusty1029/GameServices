
namespace Game.Dto.Goal
{
    public class GoalDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Achieved { get; set; }
    }
}
