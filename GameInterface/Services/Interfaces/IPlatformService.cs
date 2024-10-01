using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<ApiResult<Guid>> CreatePlatform(CancellationToken cancellationToken, string platformName);
        Task<ApiResult> DeletePlatform(CancellationToken cancellationToken, Guid id);
        Task<ApiResult<List<PlatformDto>>> GetAllPlatforms(CancellationToken cancellationToken);
        Task<ApiResult> UpdatePlatform(CancellationToken cancellationToken, Guid id, string platformName);
    }
}
