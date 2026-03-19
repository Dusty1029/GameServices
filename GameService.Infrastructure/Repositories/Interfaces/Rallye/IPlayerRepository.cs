using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities.Rallye;

namespace GameService.Infrastructure.Repositories.Interfaces.Rallye
{
    public interface IPlayerRepository : IGenericRepository<GameContext, PlayerEntity>
    {
    }
}
