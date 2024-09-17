using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<List<PlatformDto>> GetAllPlatforms();
    }
}
