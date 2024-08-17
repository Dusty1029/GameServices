using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class AchievementRepository : GenericRepository<GameContext, AchievementEntity>, IAchievementRepository
    {
        public AchievementRepository(GameContext context, ICancellationTokenService cancellationTokenService) : base(context, cancellationTokenService)
        {
        }
    }
}
