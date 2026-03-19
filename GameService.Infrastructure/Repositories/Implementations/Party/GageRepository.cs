using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities.Party;
using GameService.Infrastructure.Repositories.Interfaces.Party;

namespace GameService.Infrastructure.Repositories.Implementations.Party
{
    public class GageRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, GageEntity>(context, cancellationTokenService), IGageRepository
    {
    }
}
