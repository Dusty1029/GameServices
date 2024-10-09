using Game.Dto.Enums;

namespace Game.Dto
{
    public class UpdateGameDetailDto
    {
        public Guid Id { get; set; }
        public GameDetailStatusEnumDto Status { get; set; }
    }
}
