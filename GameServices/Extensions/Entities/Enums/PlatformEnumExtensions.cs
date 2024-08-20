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
            PlatformEnumEntity.PS1 => PlatformEnumDto.Ps1,
            PlatformEnumEntity.PS2 => PlatformEnumDto.Ps2,
            PlatformEnumEntity.PS3 => PlatformEnumDto.Ps3,
            PlatformEnumEntity.PS4 => PlatformEnumDto.Ps4,
            PlatformEnumEntity.Gamecube => PlatformEnumDto.Gamecube,
            PlatformEnumEntity.Wii => PlatformEnumDto.Wii,
            PlatformEnumEntity.Switch => PlatformEnumDto.Switch,
            PlatformEnumEntity.GameboyColor => PlatformEnumDto.GameboyColor,
            PlatformEnumEntity.GameboyAdvance => PlatformEnumDto.GameboyAdvance,
            PlatformEnumEntity.Ds => PlatformEnumDto.Ds,
            PlatformEnumEntity.ThreeDs => PlatformEnumDto.ThreeDs,
            PlatformEnumEntity.PSVITA => PlatformEnumDto.PSVITA,
            PlatformEnumEntity.PS5 => PlatformEnumDto.PS5,
            _ => throw new NotImplementedException($"The PlatformEnum [{platformEnumEntity}] is not implemented."),
        };

        public static PlatformEnumEntity ToEntity(this PlatformEnumDto platformEnumEntity) => platformEnumEntity switch
        {
            PlatformEnumDto.Steam => PlatformEnumEntity.Steam,
            PlatformEnumDto.Epic => PlatformEnumEntity.Epic,
            PlatformEnumDto.Ps1 => PlatformEnumEntity.PS1,
            PlatformEnumDto.Ps2 => PlatformEnumEntity.PS2,
            PlatformEnumDto.Ps3 => PlatformEnumEntity.PS3,
            PlatformEnumDto.Ps4 => PlatformEnumEntity.PS4,
            PlatformEnumDto.Gamecube => PlatformEnumEntity.Gamecube,
            PlatformEnumDto.Wii => PlatformEnumEntity.Wii,
            PlatformEnumDto.Switch => PlatformEnumEntity.Switch,
            PlatformEnumDto.GameboyColor => PlatformEnumEntity.GameboyColor,
            PlatformEnumDto.GameboyAdvance => PlatformEnumEntity.GameboyAdvance,
            PlatformEnumDto.Ds => PlatformEnumEntity.Ds,
            PlatformEnumDto.ThreeDs => PlatformEnumEntity.ThreeDs,
            PlatformEnumDto.PSVITA => PlatformEnumEntity.PSVITA,
            PlatformEnumDto.PS5 => PlatformEnumEntity.PS5,
            _ => throw new NotImplementedException($"The PlatformEnum [{platformEnumEntity}] is not implemented."),
        };
    }
}
