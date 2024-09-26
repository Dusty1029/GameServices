using Game.Dto;
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
            TrophyTitlePlatform = gamePlaystation.trophyTitlePlatform
        };

        public static GameEntity ToEntity(this PlaystationGameDto gamePlaystationDto, List<Trophy> trophies, List<TrophyEarned> trophiesEarned, Guid platformId) => new()
        {
            Name = gamePlaystationDto.TrophyTitleName,
            GameDetails = 
            [
                new()
                {
                    PlaystationId = gamePlaystationDto.PlaystationId,
                    PlatformId = platformId,
                    Achievements = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList()
                }
            ]
        };

        public static IgnoredGameEntity ToEntity(this PlaystationGameDto gamePlaystationDto) => new()
        {
            Name = gamePlaystationDto.TrophyTitleName,
            PlaystationId = gamePlaystationDto.PlaystationId
        };
    }
}
