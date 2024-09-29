using CommonV2.Infrastructure.Repository;
using CommonV2.Infrastructure.Services.Interfaces;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using GameService.Infrastructure.Repositories.Interfaces;

namespace GameService.Infrastructure.Repositories.Implementations
{
    public class ParameterRepository(GameContext context, ICancellationTokenService cancellationTokenService) :
        GenericRepository<GameContext, ParameterEntity>(context, cancellationTokenService), IParameterRepository
    {
        public Task<string?> GetPlaystationToken() => 
            FindSelect(p => p.ParameterEnum == ParameterEnumEntity.PlaystationToken, f => f.Select(p => p.Value));

        public Task<ParameterEntity?> GetPlaystationTokenEntity() =>
            Find(p => p.ParameterEnum == ParameterEnumEntity.PlaystationToken, noTracking: false);

        public Task<string?> GetNpsso() =>
            FindSelect(p => p.ParameterEnum == ParameterEnumEntity.Npsso, f => f.Select(p => p.Value));

        public Task<ParameterEntity?> GetNpssoEntity() =>
            Find(p => p.ParameterEnum == ParameterEnumEntity.Npsso, noTracking: false);
    }
}
