using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class CategoryRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, CategoryEntity>(context, cancellationTokenService), ICategoryRepository
    {
    }
}
