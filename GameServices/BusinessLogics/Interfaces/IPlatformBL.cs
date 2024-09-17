using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPlatformBL
    {
        Task<List<PlatformDto>> GetAllPlatforms();
    }
}
