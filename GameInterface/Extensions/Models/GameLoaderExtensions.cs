using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Extensions.Models
{
    public static class GameLoaderExtensions
    {
        public static GameLoader ToGameLoader(this SteamGameDto steamGameDto) => new() 
        { 
            Id = steamGameDto.SteamId.ToString(),
            Name = steamGameDto.Name,
            Platform = "Steam"
        };

        public static GameLoader ToGameLoader(this PlaystationGameDto playstationGame) => new()
        {
            Id = playstationGame.PlaystationId,
            Name = playstationGame.TrophyTitleName,
            Platform = playstationGame.TrophyTitlePlatform
        };
    }
}
