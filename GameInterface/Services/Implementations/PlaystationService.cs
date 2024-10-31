using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlaystationService(IGenericService genericService) : IPlaystationService
    {
        private readonly string beginPath = "playstation";
        public Task<ApiResult<Guid>> AddPlaystationGame(CancellationToken cancellationToken, CreatePlaystationGameDto gamePlaystationDto) =>
            genericService.PostResult<Guid>(cancellationToken, gamePlaystationDto, beginPath);

        public Task<ApiResult<List<PlaystationGameDto>>> GetMissingPlaystationGames(CancellationToken cancellationToken, bool forceReload = false) =>
            genericService.GetResult<List<PlaystationGameDto>>(cancellationToken, $"{beginPath}/force/{forceReload}");

        public Task<ApiResult> IgnorePlaystationGame(CancellationToken cancellationToken, PlaystationGameDto gamePlaystationDto, bool isIgnored) =>
            genericService.PostResult(cancellationToken, gamePlaystationDto, $"{beginPath}/ignore/{isIgnored}");

        public Task<ApiResult> RefreshToken(CancellationToken cancellationToken, string npsso) =>
            genericService.PutResult(cancellationToken, path: $"{beginPath}/token/{npsso}");

        public Task<ApiResult> ReloadPlaystationGame(CancellationToken cancellationToken, Guid gameDetailId) =>
            genericService.PutResult(cancellationToken, path: $"{beginPath}/game/{gameDetailId}/reload");
    }
}
