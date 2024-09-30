using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<ApiResult<Guid>> CreatePlatform(string platformName);
        Task<ApiResult> DeletePlatform(Guid id);
        Task<ApiResult<List<PlatformDto>>> GetAllPlatforms();
        Task<ApiResult> UpdatePlatform(Guid id, string platformName);
    }
}
