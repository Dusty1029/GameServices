using Game.Dto;
using GameService.API.Models.PlaystationGateway;
using GameService.API.Models.SteamGateway;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class GameExtensions
    {
        public static GameDto ToDto(this GameDetailEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Game?.Name,
            Platform = entity.Platform?.Name,
            Categories = entity.Game?.Categories?.Select(c => c.ToDto())?.ToList(),
            Achievements = entity.Achievements?.Select(a => a.ToDto())?.ToList()
        };

        public static GameEntity ToEntity(this GameDto gameDto, GameEntity? gameEntity = null)
        {
            gameEntity ??= new();

            gameEntity.Id = gameDto.Id;
            gameEntity.Name = gameDto.Name;
            //gameEntity.Platform = gameDto.Platform.ToEntity();

            return gameEntity;
        }

        public static GameEntity ToEntity(this ResponseAchievementSteam responseAchievementSteamDto, int appId, List<AchievementPercentageSteam> achievementPercentages) => new()
        {
            Name = responseAchievementSteamDto.gameName,
            //Platform = PlatformEnumEntity.Steam,
            //SteamId = appId,
            //Achievements = responseAchievementSteamDto.achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList()
        };

        public static GameEntity ToEntity(this GamePlaystation gamePlaystationDto) => new()
        {
            Name = gamePlaystationDto.trophyTitleName,
            //Platform = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform),
            //PlaystationId = gamePlaystationDto.npCommunicationId
        };

        public static GameEntity ToEntity(this GamePlaystation gamePlaystationDto, List<Trophy> trophies, List<TrophyEarned> trophiesEarned) => new()
        {
            Name = gamePlaystationDto.trophyTitleName,
            //Platform = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform),
            //PlaystationId = gamePlaystationDto.npCommunicationId,
            //Achievements = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList()
        };

        public static GameDto ToDto(this GameEntity gameEntity) => new()
        {
            Id = gameEntity.Id,
            Name = gameEntity.Name,
            //Platform = gameEntity.Platform.ToDto(),
            Categories = gameEntity.Categories?.Select(c => c.ToDto())?.ToList(),
            //Achievements = gameEntity.Achievements?.Select(a => a.ToDto())?.ToList()
        };
    }
}
