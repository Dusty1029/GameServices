using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<ApiResult<List<PlatformDto>>> GetAllPlatforms();
    }
}
