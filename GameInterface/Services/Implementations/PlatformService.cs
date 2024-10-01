using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlatformService(IGenericService genericService) : IPlatformService
    {
        private readonly string beginPath = "platform";

        public Task<ApiResult<Guid>> CreatePlatform(CancellationToken cancellationToken, string platformName) =>
            genericService.PostResult<Guid>(cancellationToken, platformName, beginPath);

        public Task<ApiResult> DeletePlatform(CancellationToken cancellationToken, Guid id) =>
            genericService.DeleteResult(cancellationToken, $"{beginPath}/{id}");

        public Task<ApiResult<List<PlatformDto>>> GetAllPlatforms(CancellationToken cancellationToken) =>
            genericService.GetResult<List<PlatformDto>>(cancellationToken, beginPath);

        public Task<ApiResult> UpdatePlatform(CancellationToken cancellationToken, Guid id, string platformName) =>
            genericService.PutResult(cancellationToken, platformName, $"{beginPath}/{id}");
    }
}
