using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPlatformBL
    {
        Task<Guid> CreatePlatform(string platformName);
        Task DeletePlatform(Guid id);
        Task<List<PlatformDto>> GetAllPlatforms();
        Task UpdatePlatform(Guid id, string platformName);
    }
}
