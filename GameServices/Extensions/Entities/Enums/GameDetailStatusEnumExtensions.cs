using Game.Dto.Enums;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.Extensions.Entities.Enums
{
    public static class GameDetailStatusEnumExtensions
    {
        public static GameDetailStatusEnumEntity ToEntity(this GameDetailStatusEnumDto dto) => dto switch
        {
            GameDetailStatusEnumDto.NotStarted => GameDetailStatusEnumEntity.NotStarted,
            GameDetailStatusEnumDto.Started => GameDetailStatusEnumEntity.Started,
            GameDetailStatusEnumDto.Finished => GameDetailStatusEnumEntity.Finished,
            GameDetailStatusEnumDto.TotalyFinished => GameDetailStatusEnumEntity.TotalyFinished,
            _ => throw new NotImplementedException($"The GameDetailStatusEnum [{dto}] is not implemented."),
        };

        public static GameDetailStatusEnumDto ToDto(this GameDetailStatusEnumEntity entity) => entity switch
        {
            GameDetailStatusEnumEntity.NotStarted => GameDetailStatusEnumDto.NotStarted,
            GameDetailStatusEnumEntity.Started => GameDetailStatusEnumDto.Started,
            GameDetailStatusEnumEntity.Finished => GameDetailStatusEnumDto.Finished,
            GameDetailStatusEnumEntity.TotalyFinished => GameDetailStatusEnumDto.TotalyFinished,
            _ => throw new NotImplementedException($"The GameDetailStatusEnum [{entity}] is not implemented."),
        };
    }
}
