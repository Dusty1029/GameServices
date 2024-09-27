using Game.Dto;
using Game.Dto.Enums;
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
    }
}
