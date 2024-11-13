
namespace Game.Dto.Goal
{
    public class CreateGoalDto
    {
        public Guid GameDetailId { get; set; }
        public required string Name { get; set; }
    }
}
