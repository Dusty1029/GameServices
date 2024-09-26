using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISteamService
    {
        Task<ApiResult<Guid>> AddSteamGame(SteamGameDto gameSteamDto);
        Task<ApiResult<List<SteamGameDto>>> GetMissingSteamGames();
        Task<ApiResult> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored);
        Task<ApiResult> ReloadSteamGame(Guid gameDetailId);
    }
}
