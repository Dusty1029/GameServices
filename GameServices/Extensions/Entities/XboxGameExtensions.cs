using Game.Dto;
using GameService.API.Models.XboxGateway;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class XboxGameExtensions
    {
        public static GameDetailEntity ToEntityWithGameId(this CreateXboxGameDto xboxGameDto, List<XboxAchievement> achievements, List<XboxAchievement> achievementsEarned, Guid platformId) => new()
        {
            GameId = xboxGameDto.GameId!.Value,
            XboxId = xboxGameDto.XboxGame.XboxId,
            PlatformId = platformId,
            Achievements = achievements.Select(a => a.ToEntity(achievementsEarned.Any(ae => ae.id == a.id))).ToList()
        };

        public static GameEntity ToEntity(this CreateXboxGameDto xboxPlaystationDto, List<XboxAchievement> achievements, List<XboxAchievement> achievementsEarned, Guid platformId) => new()
        {
            Name = xboxPlaystationDto.XboxGame.Name,
            SerieId = xboxPlaystationDto.Serie?.Id,
            GameDetails =
            [
                new()
                {
                    XboxId = xboxPlaystationDto.XboxGame.XboxId,
                    PlatformId = platformId,
                    Achievements = achievements.Select(a => a.ToEntity(achievementsEarned.Any(ae => ae.id == a.id))).ToList()
                }
            ]
        };

        public static IgnoredGameEntity ToEntity(this XboxGameDto xboxGameDto) => new()
        {
            Name = xboxGameDto.Name,
            XboxId = xboxGameDto.XboxId
        };
    }
}
