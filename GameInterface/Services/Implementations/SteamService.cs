using Game.Dto.Steam;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class SteamService(IGenericService genericService) : ISteamService
    {
        private readonly string beginPath = "steam";
        public Task<ApiResult<Guid>> AddSteamGame(CancellationToken cancellationToken, CreateSteamGameDto gameSteamDto) =>
            genericService.PostResult<Guid>(cancellationToken, gameSteamDto, beginPath);

        public Task<ApiResult<List<SteamGameDto>>> GetMissingSteamGames(CancellationToken cancellationToken, bool forceReload) =>
            genericService.GetResult<List<SteamGameDto>>(cancellationToken, $"{beginPath}/force/{forceReload}");

        public Task<ApiResult> IgnoreSteamGame(CancellationToken cancellationToken, SteamGameDto gameSteamDto, bool isIgnored) =>
            genericService.PostResult(cancellationToken, gameSteamDto, $"{beginPath}/ignore/{isIgnored}");

        public Task<ApiResult> ReloadSteamGame(CancellationToken cancellationToken, Guid gameDetailId) =>
            genericService.PutResult(cancellationToken, path: $"{beginPath}/game/{gameDetailId}/reload");
    }
}
