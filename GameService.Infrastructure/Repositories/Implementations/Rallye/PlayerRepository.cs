using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities.Rallye;
using GameService.Infrastructure.Repositories.Interfaces.Rallye;

namespace GameService.Infrastructure.Repositories.Implementations.Rallye
{
    public class PlayerRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, PlayerEntity>(context, cancellationTokenService), IPlayerRepository
    {
    }
}
