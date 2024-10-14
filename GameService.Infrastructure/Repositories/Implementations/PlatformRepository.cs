using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class PlatformRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, PlatformEntity>(context, cancellationTokenService), IPlatformRepository
    {
        public Task<Guid> GetPlatformIdByEnum(PlatformEnumEntity platformEnum) =>
            FindSelect(p => p.PlatformEnum == platformEnum, f => f.Select(p => p.Id));
    }
}
