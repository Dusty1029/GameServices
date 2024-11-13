using Game.Dto.Enums;

namespace Game.Dto.Games
{
    public class UpdateGameDetailDto
    {
        public Guid Id { get; set; }
        public GameDetailStatusEnumDto Status { get; set; }
    }
}
