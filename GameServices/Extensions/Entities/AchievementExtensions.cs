using GameService.Infrastructure.Entities;
using GameServices.API.Dtos;
using GameServices.API.Dtos.SteamGateway;

namespace GameServices.API.Extensions.Entities
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
            achievementEntity.GameId = achievementDto.GameId;

            return achievementEntity;
        }

        public static AchievementEntity ToEntity(this AchievementSteamDto achievementSteamDto, decimal? percentage) => new()
        {
            Achieved = achievementSteamDto.achieved != 0,
            Description = achievementSteamDto.description,
            Name = achievementSteamDto.name,
            SteamName = achievementSteamDto.apiname,
            Percentage = percentage
        };

        public static AchievementDto ToDto(this AchievementEntity achievementEntity) => new()
        {
            Id = achievementEntity.Id,
            Description = achievementEntity.Description,
            Name = achievementEntity.Name,
            GameId = achievementEntity.GameId,
            Percentage = achievementEntity.Percentage,
            Achievement = achievementEntity.Achieved
        };
    }
}
