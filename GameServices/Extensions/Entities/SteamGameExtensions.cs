﻿using Game.Dto;
using GameService.API.Models.SteamGateway;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.API.Extensions.Entities
{
    public static class SteamGameExtensions
    {
        public static SteamGameDto ToDto(this GameSteam gameSteam) => new SteamGameDto
        {
            Name = gameSteam.name,
            SteamId = gameSteam.appid
        };

        public static GameEntity ToEntity(this SteamGameDto gameSteamDto, List<AchievementSteam> achievements, List<AchievementPercentageSteam> achievementPercentages, Guid platformId) => new()
        {
            Name = gameSteamDto.Name,
            GameDetails =
            [
                new() 
                {
                    SteamId = gameSteamDto.SteamId,
                    Achievements = achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList(),
                    PlatformId = platformId
                }
            ]
        };

        public static IgnoredSteamGameEntity ToEntity(this SteamGameDto steamGameDto) => new()
        {
            Name = steamGameDto.Name,
            SteamId = steamGameDto.SteamId
        };
    }
}
