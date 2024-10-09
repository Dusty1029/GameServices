using Game.Dto;
using GameService.API.Extensions.Entities.Enums;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class GameDetailExtensions
    {
        public static GameDetailDto ToDto(this GameDetailEntity gameDetail) => new()
        {
            Id = gameDetail.Id,
            Platform = gameDetail.Platform!.ToDto(),
            Achievements = gameDetail.Achievements!.Select(a => a.ToDto()),
            AchievementCompletion = gameDetail.Achievements!.Count != 0 ?
                Math.Round((decimal)gameDetail.Achievements!.Count(a => a.IsIgnored || a.Achieved) / (decimal)gameDetail.Achievements!.Count * 100m, 2) : null,
            Status = gameDetail.Status.ToDto()
        };

        public static void ToEntity(this UpdateGameDetailDto gameDetailDto, GameDetailEntity gameDetailEntity)
        {
            gameDetailEntity.Status = gameDetailDto.Status.ToEntity();
        }
    }
}
