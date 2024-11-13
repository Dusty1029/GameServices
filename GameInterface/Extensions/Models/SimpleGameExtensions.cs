using Game.Dto.Enums;
using Game.Dto.Playstation;
using Game.Dto.Steam;
using Game.Dto.Xbox;
using GameInterface.Models;

namespace GameInterface.Extensions.Models
{
    public static class SimpleGameExtensions
    {
        public static SimpleGame ToGameLoader(this SteamGameDto steamGameDto) => new() 
        { 
            Id = steamGameDto.SteamId.ToString(),
            Name = steamGameDto.Name,
            Platform = PlatformEnumDto.Steam
        };

        public static SimpleGame ToGameLoader(this PlaystationGameDto playstationGame) => new()
        {
            Id = playstationGame.PlaystationId,
            Name = playstationGame.TrophyTitleName,
            Platform = playstationGame.TrophyTitlePlatform
        };

        public static SimpleGame ToGameLoader(this XboxGameDto xboxGame) => new()
        {
            Id = xboxGame.XboxId,
            Name = xboxGame.Name,
            Platform = xboxGame.PlatformEnum
        };
    }
}
