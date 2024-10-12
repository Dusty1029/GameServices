using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities;

namespace GameService.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<GameContext, GameEntity>
    {
        Task<GameEntity> CreateGame(GameEntity gameEntity, List<CategoryEntity>? categories);
        Task<int> MaxPlayOrderBySerie(Guid serieId);
    }
}
