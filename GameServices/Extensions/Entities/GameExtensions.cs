using Game.Dto;
using GameService.API.Extensions.Entities.Enums;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Extensions.Enums;
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
            GameDetails = entity.GameDetails!.Select(gd => gd.ToDto()).OrderByDescending(gd => gd.AchievementCompletion),
            Serie =  entity.Serie?.ToSimpleDto()
        };

        public static SimpleGameDto ToSimpleDto(this GameEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Order = entity.PlayOrder
        };

        public static SearchGameItemDto ToSearchItemDto(this GameEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Platforms = string.Join(", ", entity.GameDetails!.Select(gd => gd.Platform!.Name)),
            Categories = string.Join(", ", entity.Categories!.Select(c => c.Name)),
            Serie = entity.Serie?.Name ?? string.Empty,
            Status = entity.GameDetails!.OrderBy(gd => gd.Status.GetOrderList()).Select(gd => gd.Status.ToDto()).Distinct()
        };

        public static GameEntity ToEntity(this CreateGameDto createGame) => new()
        {
            Name = createGame.Name,
            SerieId = createGame.Serie?.Id,
            GameDetails =
            [
                new()
                {
                    PlatformId = createGame.Platform!.Id,
                    Status = createGame.Status.HasValue ? createGame.Status.Value.ToEntity() : GameDetailStatusEnumEntity.NotStarted
                }
            ]
        };

        public static void ToEntity(this UpdateGameDto gameDto, GameEntity gameEntity)
        {
            gameEntity.Name = gameDto.Name;
            gameEntity.SerieId = gameDto.Serie?.Id;
            gameDto.GameDetails.ForEach(gd => gd.ToEntity(gameEntity.GameDetails!.First(gde => gde.Id == gd.Id)));
        }

        public static void UpdateStatusOrderGameEntity(this GameEntity gameEntity) =>
            gameEntity.StatusOrder = gameEntity.GameDetails!.Where(gd => gd.Status != GameDetailStatusEnumEntity.NotStarted)
                                                            .Select(gd => gd.Status.GetOrder())
                                                            .DefaultIfEmpty(GameDetailStatusEnumEntity.NotStarted.GetOrder())
                                                            .Max();
    }
}
