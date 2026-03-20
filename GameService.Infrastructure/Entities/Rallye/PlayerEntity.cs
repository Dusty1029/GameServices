namespace GameService.Infrastructure.Entities.Rallye
{
    public class PlayerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<RallyeEntity>? Rallyes { get; set; }
        public List<SpecialTimeEntity>? SpecialTimes { get; set; }
    }
}
