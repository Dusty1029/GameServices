using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlatformService(IGenericService genericService) : IPlatformService
    {
        private readonly string beginPath = "platform";

        public Task<ApiResult<Guid>> CreatePlatform(string platformName) => genericService.PostResult<Guid>(platformName, beginPath);

        public Task<ApiResult> DeletePlatform(Guid id) => genericService.DeleteResult($"{beginPath}/{id}");

        public Task<ApiResult<List<PlatformDto>>> GetAllPlatforms() => genericService.GetResult<List<PlatformDto>>(beginPath);

        public Task<ApiResult> UpdatePlatform(Guid id, string platformName) => genericService.PutResult(platformName, $"{beginPath}/{id}");
    }
}
