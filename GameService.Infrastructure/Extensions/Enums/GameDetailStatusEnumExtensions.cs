using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Extensions.Enums
{
    public static class GameDetailStatusEnumExtensions
    {
        private readonly static List<GameDetailStatusEnumEntity> OrderedGameDetailStatus =
        [
            GameDetailStatusEnumEntity.Started,
            GameDetailStatusEnumEntity.Finished,
            GameDetailStatusEnumEntity.TotalyFinished,
            GameDetailStatusEnumEntity.NotStarted,
            GameDetailStatusEnumEntity.ToBuy
        ];

        private readonly static List<GameDetailStatusEnumEntity> ListOrderedGameDetailStatus =
        [
            GameDetailStatusEnumEntity.TotalyFinished,
            GameDetailStatusEnumEntity.Finished,
            GameDetailStatusEnumEntity.Started,
            GameDetailStatusEnumEntity.NotStarted,
            GameDetailStatusEnumEntity.ToBuy,
        ];

        public static int GetOrder(this GameDetailStatusEnumEntity status) => OrderedGameDetailStatus.FindIndex(s => s == status);
        public static int GetOrderList(this GameDetailStatusEnumEntity status) => ListOrderedGameDetailStatus.FindIndex(s => s == status);
    }
}
