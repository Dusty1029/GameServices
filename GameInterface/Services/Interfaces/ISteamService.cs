using Game.Dto.Steam;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISteamService
    {
        Task<ApiResult<Guid>> AddSteamGame(CancellationToken cancellationToken, CreateSteamGameDto gameSteamDto);
        Task<ApiResult<List<SteamGameDto>>> GetMissingSteamGames(CancellationToken cancellationToken, bool forceReload = false);
        Task<ApiResult> IgnoreSteamGame(CancellationToken cancellationToken, SteamGameDto gameSteamDto, bool isIgnored);
        Task<ApiResult> ReloadSteamGame(CancellationToken cancellationToken, Guid gameDetailId);
    }
}
