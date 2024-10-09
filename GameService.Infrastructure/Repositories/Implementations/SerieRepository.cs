using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class SerieRepository(GameContext context, ICancellationTokenService cancellationTokenService) : GenericRepository<GameContext, SerieEntity>(context, cancellationTokenService), ISerieRepository
    {
        public Task<SerieEntity> FindDefaultSerie() => Find(s => s.IsDefault)!;
    }
}
