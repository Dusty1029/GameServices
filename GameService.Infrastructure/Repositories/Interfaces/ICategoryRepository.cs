using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities;

namespace GameService.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<GameContext, CategoryEntity>
    {
    }
}
