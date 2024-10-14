using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;

namespace GameService.Infrastructure.Repositories.Interfaces
{
    public interface IPlatformRepository : IGenericRepository<GameContext, PlatformEntity>
    {
        Task<Guid> GetPlatformIdByEnum(PlatformEnumEntity platformEnum);
    }
}
