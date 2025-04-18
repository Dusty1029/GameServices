﻿using Game.Dto.Steam;
using GameService.API.Models.SteamGateway;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class SteamGameExtensions
    {
        public static SteamGameDto ToDto(this GameSteam gameSteam) => new SteamGameDto
        {
            Name = gameSteam.name,
            SteamId = gameSteam.appid
        };

        public static GameEntity ToEntity(this CreateSteamGameDto gameSteamDto, List<AchievementSteam> achievements, List<AchievementPercentageSteam> achievementPercentages, Guid platformId) => new()
        {
            Name = gameSteamDto.SteamGame.Name,
            SerieId = gameSteamDto.Serie?.Id,
            GameDetails =
            [
                new() 
                {
                    SteamId = gameSteamDto.SteamGame.SteamId,
                    Achievements = achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList(),
                    PlatformId = platformId
                }
            ]
        };

        public static GameDetailEntity ToEntityWithGameId(this CreateSteamGameDto gameSteamDto, List<AchievementSteam> achievements, List<AchievementPercentageSteam> achievementPercentages, Guid platformId) => new()
        {
            GameId = gameSteamDto.GameId!.Value,
            SteamId = gameSteamDto.SteamGame.SteamId,
            Achievements = achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList(),
            PlatformId = platformId
        };

        public static IgnoredGameEntity ToEntity(this SteamGameDto steamGameDto) => new()
        {
            Name = steamGameDto.Name,
            SteamId = steamGameDto.SteamId
        };
    }
}
