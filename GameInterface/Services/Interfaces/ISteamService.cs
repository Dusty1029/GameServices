using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISteamService
    {
        Task<ApiResult<Guid>> AddSteamGame(CreateSteamGameDto gameSteamDto);
        Task<ApiResult<List<SteamGameDto>>> GetMissingSteamGames();
        Task<ApiResult> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored);
        Task<ApiResult> ReloadSteamGame(Guid gameDetailId);
    }
}
