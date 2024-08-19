using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class ParameterRepository : GenericRepository<GameContext, ParameterEntity>, IParameterRepository
    {
        public ParameterRepository(GameContext context, ICancellationTokenService cancellationTokenService) : base(context, cancellationTokenService)
        {
        }

        public Task<string?> GetPlaystationToken() => 
            FindSelect(p => p.ParameterEnum == ParameterEnumEntity.PlaystationToken, f => f.Select(p => p.Value));

        public Task<ParameterEntity?> GetPlaystationTokenEntity() =>
            Find(p => p.ParameterEnum == ParameterEnumEntity.PlaystationToken, noTracking: false);
    }
}
