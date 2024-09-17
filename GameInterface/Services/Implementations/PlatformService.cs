using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlatformService(HttpClient httpClient) : IPlatformService
    {
        public Task<List<PlatformDto>> GetAllPlatforms() => httpClient.Get<List<PlatformDto>>();
    }
}
