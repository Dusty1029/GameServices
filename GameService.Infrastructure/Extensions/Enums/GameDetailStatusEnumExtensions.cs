using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Extensions.Enums
{
    public static class GameDetailStatusEnumExtensions
    {
        private readonly static List<GameDetailStatusEnumEntity> ListOrderedGameDetailStatus =
        [
            GameDetailStatusEnumEntity.TotalyFinished,
            GameDetailStatusEnumEntity.Finished,
            GameDetailStatusEnumEntity.Started,
            GameDetailStatusEnumEntity.NotStarted,
            GameDetailStatusEnumEntity.ToBuy
        ];

        private readonly static List<GameDetailStatusEnumEntity> ListStatusOrder =
        [
            GameDetailStatusEnumEntity.ToBuy,
            GameDetailStatusEnumEntity.NotStarted,
            GameDetailStatusEnumEntity.Finished,
            GameDetailStatusEnumEntity.TotalyFinished,
            GameDetailStatusEnumEntity.Started
        ];

        public static int GetOrderList(this GameDetailStatusEnumEntity status) => ListOrderedGameDetailStatus.FindIndex(s => s == status);
        public static int GetStatusOrder(this GameDetailStatusEnumEntity status) => ListStatusOrder.FindIndex(s => s == status);
    }
}
