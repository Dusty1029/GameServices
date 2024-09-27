using Game.Dto.Enums;

namespace GameInterface.Models
{
    public class SimpleGame
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public PlatformEnumDto? Platform { get; set; }
    }
}
