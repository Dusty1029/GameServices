using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class SerieRepository(GameContext context, ICancellationTokenService cancellationTokenService) : GenericRepository<GameContext, SerieEntity>(context, cancellationTokenService), ISerieRepository
    {
        public Task<SerieEntity> FindDefaultSerie() => Find(s => s.IsDefault)!;

        public Task<List<SerieEntity>> GetSeriesWithOrderedGames()
        {
            return DbSet.AsQueryable()
                        .AsNoTracking()
                        .Where(s => s.Games!.Count > 0)
                        .Include(s => s.Games!).ThenInclude(g => g.GameDetails!).ThenInclude(gd => gd.Platform)
                        .Include(s => s.Games!).ThenInclude(g => g.Categories)
                        .Select(s => new
                        {
                            Entity = s,
                            IsDefaultScore = !s.IsDefault ? 1 : 0,

                            HasStarted = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.Started),
                            HasStartedTotallyFinished = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.StartedTotalyFinished),
                            HasFinished = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.Finished),
                            HasNotStarted = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.NotStarted),
                            HasToBuy = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.ToBuy),
                            HasBreak = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.Break),
                            HasTotallyFinished = s.Games!.Any(g => g.GlobalStatus == GameDetailStatusEnumEntity.TotalyFinished),

                            AllFinished = s.Games!.All(g => g.GlobalStatus == GameDetailStatusEnumEntity.Finished),
                            AllNotStarted = s.Games!.All(g => g.GlobalStatus == GameDetailStatusEnumEntity.NotStarted),
                            AllToBuy = s.Games!.All(g => g.GlobalStatus == GameDetailStatusEnumEntity.ToBuy),
                            AllTotallyFinished = s.Games!.All(g => g.GlobalStatus == GameDetailStatusEnumEntity.TotalyFinished),
                        })
                        .OrderByDescending(x => x.IsDefaultScore)
                        .ThenByDescending(x =>
                            x.HasStarted ? 100 :
                            x.HasStartedTotallyFinished ? 90 :
                            x.HasBreak ? 85 :
                            ((x.HasFinished || x.HasTotallyFinished) && (x.HasNotStarted || x.HasToBuy)) ? 80 :
                            x.AllFinished ? 70 :
                            x.AllNotStarted ? 60 :
                            (x.HasNotStarted && x.HasToBuy) ? 50 :
                            x.AllToBuy ? 40 :
                            x.AllTotallyFinished ? 30 :
                            0
                        )
                        .ThenBy(x => x.Entity.Name)
                        .Select(s => s.Entity)
                        .ToListAsync();
        }
    }
}
