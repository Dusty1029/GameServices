using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities.Party;

namespace GameService.Infrastructure.Repositories.Interfaces.Party
{
    public interface IGameRepository : IGenericRepository<GameContext, GameEntity>
    {
    }
}
