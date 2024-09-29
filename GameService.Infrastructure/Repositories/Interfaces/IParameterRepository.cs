using CommonV2.Infrastructure.Repository;
using GameService.Infrastructure.Entities;

namespace GameService.Infrastructure.Repositories.Interfaces
{
    public interface IParameterRepository : IGenericRepository<GameContext, ParameterEntity>
    {
        Task<string?> GetPlaystationToken();
        Task<ParameterEntity?> GetPlaystationTokenEntity();
        Task<string?> GetNpsso();
        Task<ParameterEntity?> GetNpssoEntity();
    }
}
