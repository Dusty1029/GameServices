using GameService.Infrastructure.Entities.Enums;
using GameServices.API.Dtos.Enums;

namespace GameServices.API.Extensions.Entities.Enums
{
    public static class PlatformEnumExtensions
    {
        public static PlatformEnumDto ToDto(this PlatformEnumEntity platformEnumEntity) => platformEnumEntity switch
        {
            PlatformEnumEntity.Steam => PlatformEnumDto.Steam,
            PlatformEnumEntity.Epic => PlatformEnumDto.Epic,
            PlatformEnumEntity.Ps1 => PlatformEnumDto.Ps1,
            PlatformEnumEntity.Ps2 => PlatformEnumDto.Ps2,
            PlatformEnumEntity.Ps3 => PlatformEnumDto.Ps3,
            PlatformEnumEntity.Ps4 => PlatformEnumDto.Ps4,
            PlatformEnumEntity.Gamecube => PlatformEnumDto.Gamecube,
            PlatformEnumEntity.Wii => PlatformEnumDto.Wii,
            PlatformEnumEntity.Switch => PlatformEnumDto.Switch,
            PlatformEnumEntity.GameboyColor => PlatformEnumDto.GameboyColor,
            PlatformEnumEntity.GameboyAdvance => PlatformEnumDto.GameboyAdvance,
            PlatformEnumEntity.Ds => PlatformEnumDto.Ds,
            PlatformEnumEntity.ThreeDs => PlatformEnumDto.ThreeDs,
            _ => throw new NotImplementedException($"The PlatformEnum [{platformEnumEntity}] is not implemented."),
        };

        public static PlatformEnumEntity ToEntity(this PlatformEnumDto platformEnumEntity) => platformEnumEntity switch
        {
            PlatformEnumDto.Steam => PlatformEnumEntity.Steam,
            PlatformEnumDto.Epic => PlatformEnumEntity.Epic,
            PlatformEnumDto.Ps1 => PlatformEnumEntity.Ps1,
            PlatformEnumDto.Ps2 => PlatformEnumEntity.Ps2,
            PlatformEnumDto.Ps3 => PlatformEnumEntity.Ps3,
            PlatformEnumDto.Ps4 => PlatformEnumEntity.Ps4,
            PlatformEnumDto.Gamecube => PlatformEnumEntity.Gamecube,
            PlatformEnumDto.Wii => PlatformEnumEntity.Wii,
            PlatformEnumDto.Switch => PlatformEnumEntity.Switch,
            PlatformEnumDto.GameboyColor => PlatformEnumEntity.GameboyColor,
            PlatformEnumDto.GameboyAdvance => PlatformEnumEntity.GameboyAdvance,
            PlatformEnumDto.Ds => PlatformEnumEntity.Ds,
            PlatformEnumDto.ThreeDs => PlatformEnumEntity.ThreeDs,
            _ => throw new NotImplementedException($"The PlatformEnum [{platformEnumEntity}] is not implemented."),
        };
    }
}
