using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameServices.API.Dtos;
using GameServices.API.Dtos.PlaystationGateway;
using GameServices.API.Dtos.SteamGateway;
using GameServices.API.Extensions.Entities.Enums;

namespace GameServices.API.Extensions.Entities
{
    public static class GameExtensions
    {
        public static GameEntity ToEntity(this GameDto gameDto, GameEntity? gameEntity = null)
        {
            gameEntity ??= new();

            gameEntity.Id = gameDto.Id;
            gameEntity.Name = gameDto.Name;
            gameEntity.Platform = gameDto.Platform.ToEntity();

            return gameEntity;
        }

        public static GameEntity ToEntity(this ResponseAchievementSteamDto responseAchievementSteamDto, int appId, List<AchievementPercentageSteamDto> achievementPercentages) => new()
        {
            Name = responseAchievementSteamDto.gameName,
            Platform = PlatformEnumEntity.Steam,
            SteamId = appId,
            Achievements = responseAchievementSteamDto.achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList()
        };

        public static GameEntity ToEntity(this GameSteamDto gameSteamDto, List<AchievementSteamDto> achievements, List<AchievementPercentageSteamDto> achievementPercentages) => new()
        {
            Name = gameSteamDto.name,
            Platform = PlatformEnumEntity.Steam,
            SteamId = gameSteamDto.appid,
            Achievements = achievements.Select(a => a.ToEntity(achievementPercentages.FirstOrDefault(ap => ap.name == a.apiname)?.percent)).ToList()
        };

        public static GameEntity ToEntity(this GameSteamDto gameSteamDto) => new()
        {
            Name = gameSteamDto.name,
            Platform = PlatformEnumEntity.Steam,
            SteamId = gameSteamDto.appid
        };

        public static GameEntity ToEntity(this GamePlaystationDto gamePlaystationDto) => new()
        {
            Name = gamePlaystationDto.trophyTitleName,
            Platform = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform),
            PlaystationId = gamePlaystationDto.npCommunicationId
        };

        public static GameEntity ToEntity(this GamePlaystationDto gamePlaystationDto, List<TrophyDto> trophies, List<TrophyEarnedDto> trophiesEarned) => new()
        {
            Name = gamePlaystationDto.trophyTitleName,
            Platform = Enum.Parse<PlatformEnumEntity>(gamePlaystationDto.trophyTitlePlatform),
            PlaystationId = gamePlaystationDto.npCommunicationId,
            Achievements = trophies.Select(t => t.ToEntity(trophiesEarned.First(te => te.trophyId == t.trophyId))).ToList()
        };

        public static GameDto ToDto(this GameEntity gameEntity) => new()
        {
            Id = gameEntity.Id,
            Name = gameEntity.Name,
            Platform = gameEntity.Platform.ToDto(),
            Categories = gameEntity.Categories?.Select(c => c.ToDto())?.ToList(),
            Achievements = gameEntity.Achievements?.Select(a => a.ToDto())?.ToList()
        };
    }
}
