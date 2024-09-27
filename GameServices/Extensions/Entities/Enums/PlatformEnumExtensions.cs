using Game.Dto.Enums;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.Extensions.Entities.Enums
{
    public static class PlatformEnumExtensions
    {
        public static PlatformEnumDto ToDto(this PlatformEnumEntity platformEnumEntity) => platformEnumEntity switch
        {
            PlatformEnumEntity.Steam => PlatformEnumDto.Steam,
            PlatformEnumEntity.PSVITA => PlatformEnumDto.PSVITA,
            PlatformEnumEntity.PS3 => PlatformEnumDto.PS3,
            PlatformEnumEntity.PS4 => PlatformEnumDto.PS4,
            PlatformEnumEntity.PS5 => PlatformEnumDto.PS5,
            _ => throw new NotImplementedException($"The platformEnum [{platformEnumEntity}] is not implemented."),
        };

        public static PlatformEnumEntity ToEntity(this PlatformEnumDto platformEnumEntity) => platformEnumEntity switch
        {
            PlatformEnumDto.Steam => PlatformEnumEntity.Steam,
            PlatformEnumDto.PSVITA => PlatformEnumEntity.PSVITA,
            PlatformEnumDto.PS3 => PlatformEnumEntity.PS3,
            PlatformEnumDto.PS4 => PlatformEnumEntity.PS4,
            PlatformEnumDto.PS5 => PlatformEnumEntity.PS5,
            _ => throw new NotImplementedException($"The platformEnum [{platformEnumEntity}] is not implemented."),
        };
    }
}
