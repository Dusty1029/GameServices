using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlatformService(IGenericService genericService) : IPlatformService
    {
        private readonly string beginPath = "platform";
        public Task<ApiResult<List<PlatformDto>>> GetAllPlatforms() => genericService.GetResult<List<PlatformDto>>(beginPath);
    }
}
