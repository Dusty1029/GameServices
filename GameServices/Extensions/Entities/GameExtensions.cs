using Game.Dto;
using GameService.API.Models.PlaystationGateway;
using GameService.API.Models.SteamGateway;
using GameService.Infrastructure.Entities;
using LinqKit;

namespace GameService.API.Extensions.Entities
{
    public static class GameExtensions
    {
        public static GameDto ToDto(this GameEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Categories = entity.Categories!.Select(c => c.ToDto()),
            GameDetails = entity.GameDetails!.Select(gd => gd.ToDto()).OrderByDescending(gd => gd.AchievementCompletion)
        };

        public static SearchGameItemDto ToSearchItemDto(this GameEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Platforms = string.Join(", ", entity.GameDetails!.Select(gd => gd.Platform!.Name)),
            Categories = string.Join(", ", entity.Categories!.Select(c => c.Name))
        };

        public static GameEntity ToEntity(this CreateGameDto createGame) => new()
        {
            Name = createGame.Name,
            GameDetails =
            [
                new()
                {
                    PlatformId = createGame.Platform!.Id
                }
            ]
        };

        public static void ToEntity(this UpdateGameDto gameDto, GameEntity gameEntity)
        {
            gameEntity.Name = gameDto.Name;
            gameDto.GameDetails.ForEach(gd => gd.ToEntity(gameEntity.GameDetails!.First(gde => gde.Id == gd.Id)));
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
    }
}
