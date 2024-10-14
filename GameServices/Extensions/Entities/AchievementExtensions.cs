using Game.Dto;
using GameService.API.Models.PlaystationGateway;
using GameService.API.Models.SteamGateway;
using GameService.API.Models.XboxGateway;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class AchievementExtensions
    {
        public static AchievementEntity ToEntity(this AchievementDto achievementDto, AchievementEntity? achievementEntity = null)
        {
            achievementEntity ??= new();

            achievementEntity.Id = achievementDto.Id;
            achievementEntity.Percentage = achievementDto.Percentage;
            achievementEntity.Description = achievementDto.Description;
            achievementEntity.Name = achievementDto.Name;

            return achievementEntity;
        }

        public static AchievementEntity ToEntity(this AchievementSteam achievementSteamDto, decimal? percentage = null) => new()
        {
            Achieved = achievementSteamDto.achieved != 0,
            Description = achievementSteamDto.description,
            Name = achievementSteamDto.name,
            SteamName = achievementSteamDto.apiname,
            Percentage = percentage
        };

        public static AchievementEntity ToEntity(this Trophy trophy, TrophyEarned trophyEarned) => new()
        {
             Achieved = trophyEarned.earned,
             Name = trophy.trophyName,
             PlaystationTrophyId = trophy.trophyId,
             Description = trophy.trophyDetail,
             Percentage = trophyEarned.trophyEarnedRate
        };

        public static AchievementEntity ToEntity(this XboxAchievement achievement, bool isEarned) => new()
        {
            Achieved = isEarned,
            Name = achievement.name,
            XboxId = achievement.id,
            Description = achievement.description,
            Percentage = achievement.rarity?.currentPercentage
        };

        public static AchievementDto ToDto(this AchievementEntity achievementEntity) => new()
        {
            Id = achievementEntity.Id,
            Description = achievementEntity.Description,
            Name = achievementEntity.Name,
            Percentage = achievementEntity.Percentage,
            Achieved = achievementEntity.Achieved
        };
    }
}
