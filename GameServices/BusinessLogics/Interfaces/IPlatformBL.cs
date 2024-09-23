using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPlatformBL
    {
        Task<Guid> CreatePlatform(string platformName);
        Task<List<PlatformDto>> GetAllPlatforms();
    }
}
