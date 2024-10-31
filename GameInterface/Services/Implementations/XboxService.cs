using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class XboxService(IGenericService genericService) : IXboxService
    {
        private readonly string beginPath = "xbox";

        public Task<ApiResult<Guid>> AddXboxGame(CancellationToken cancellationToken, CreateXboxGameDto xboxGameDto) =>
            genericService.PostResult<Guid>(cancellationToken, xboxGameDto, beginPath);

        public Task<ApiResult<List<XboxGameDto>>> GetMissingXboxGames(CancellationToken cancellationToken, bool forceReload = false) =>
            genericService.GetResult<List<XboxGameDto>>(cancellationToken, $"{beginPath}/force/{forceReload}");

        public Task<ApiResult> IgnoreXboxGame(CancellationToken cancellationToken, XboxGameDto xboxGameDto, bool isIgnored) =>
            genericService.PostResult(cancellationToken, xboxGameDto, $"{beginPath}/ignore/{isIgnored}");

        public Task<ApiResult> ReloadXboxGame(CancellationToken cancellationToken, Guid gameId) =>
            genericService.PutResult(cancellationToken, path: $"{beginPath}/game/{gameId}/reload");
    }
}
