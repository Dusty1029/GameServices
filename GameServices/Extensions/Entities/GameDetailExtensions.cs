using Game.Dto.Games;
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
            Goals = gameDetail.Goals!.Select(g => g.ToDto()),
            AchievementCompletion = gameDetail.CalculAchievementCompletion(),
            Status = gameDetail.Status.ToDto()
        };

        public static void ToEntity(this UpdateGameDetailDto gameDetailDto, GameDetailEntity gameDetailEntity)
        {
            gameDetailEntity.Status = gameDetailDto.Status.ToEntity();
        }

        private static decimal? CalculAchievementCompletion(this GameDetailEntity gameDetail)
        {
            if(gameDetail.Achievements!.Count == 0 && gameDetail.Goals!.Count == 0)
                return null;
            var countAchieved = (decimal)gameDetail.Achievements!.Count(a => a.IsIgnored || a.Achieved) + (decimal)gameDetail.Goals!.Count(a => a.IsFulFilled);
            var countTotal = (decimal)gameDetail.Achievements!.Count + (decimal)gameDetail.Goals!.Count;
            return Math.Round(countAchieved / countTotal * 100m, 2);
        }
    }
}
