using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IXboxService
    {
        Task<ApiResult<Guid>> AddXboxGame(CancellationToken cancellationToken, CreateXboxGameDto xboxGameDto);
        Task<ApiResult<List<XboxGameDto>>> GetMissingXboxGames(CancellationToken cancellationToken);
        Task<ApiResult> IgnoreXboxGame(CancellationToken cancellationToken, XboxGameDto xboxGameDto, bool isIgnored);
        Task<ApiResult> ReloadXboxGame(CancellationToken cancellationToken, Guid gameId);
    }
}
