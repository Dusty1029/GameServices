using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<GameContext, CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(GameContext context, ICancellationTokenService cancellationTokenService) : base(context, cancellationTokenService)
        {
        }
    }
}
