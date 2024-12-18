﻿using Game.Dto.Enums;
using Game.Dto.Playstation;
using GameService.API.Models.PlaystationGateway;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class PlaystationGameExtensions
    {
        public static PlaystationGameDto ToDto(this GamePlaystation gamePlaystation) => new()
        {
            PlaystationId = gamePlaystation.npCommunicationId,
            TrophyTitleName = gamePlaystation.trophyTitleName,
            TrophyTitlePlatform = Enum.Parse<PlatformEnumDto>(gamePlaystation.trophyTitlePlatform)
        };

        public static GameEntity ToEntity(this CreatePlaystationGameDto gamePlaystationDto, List<Trophy> trophies, List<TrophyEarned> trophiesEarned, Guid platformId) => new()
        {
            Name = gamePlaystationDto.PlaystationGame.TrophyTitleName,
            SerieId = gamePlaystationDto.Serie?.Id,
            GameDetails = 
            [
                new()
                {
                    PlaystationId = gamePlaystationDto.PlaystationGame.PlaystationId,
                    PlatformId = platformId,
                    Achievements = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList()
                }
            ]
        };

        public static GameDetailEntity ToEntityWithGameId(this CreatePlaystationGameDto gamePlaystationDto, List<Trophy> trophies, List<TrophyEarned> trophiesEarned, Guid platformId) => new()
        {
            GameId = gamePlaystationDto.GameId!.Value,
            PlaystationId = gamePlaystationDto.PlaystationGame.PlaystationId,
            PlatformId = platformId,
            Achievements = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList()
        };

        public static IgnoredGameEntity ToEntity(this PlaystationGameDto gamePlaystationDto) => new()
        {
            Name = gamePlaystationDto.TrophyTitleName,
            PlaystationId = gamePlaystationDto.PlaystationId
        };
    }
}
