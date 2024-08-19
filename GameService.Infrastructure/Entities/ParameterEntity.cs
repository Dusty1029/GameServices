using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Entities
{
    public class ParameterEntity
    {
        public Guid Id { get; set; }
        public ParameterEnumEntity ParameterEnum { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
