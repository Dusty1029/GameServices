using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class SteamService(IGenericService genericService) : ISteamService
    {
        private readonly string beginPath = "steam";
        public Task<ApiResult<Guid>> AddSteamGame(CreateSteamGameDto gameSteamDto) => genericService.PostResult<Guid>(gameSteamDto, beginPath);

        public Task<ApiResult<List<SteamGameDto>>> GetMissingSteamGames() => genericService.GetResult<List<SteamGameDto>>(beginPath);

        public Task<ApiResult> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored) => genericService.PostResult(gameSteamDto, $"{beginPath}/ignore/{isIgnored}");

        public Task<ApiResult> ReloadSteamGame(Guid gameDetailId) => genericService.PutResult(path: $"{beginPath}/game/{gameDetailId}/reload");
    }
}
