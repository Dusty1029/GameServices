using Game.Dto.Playstation;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IPlaystationService
    {
        Task<ApiResult<Guid>> AddPlaystationGame(CancellationToken cancellationToken, CreatePlaystationGameDto gamePlaystationDto);
        Task<ApiResult> IgnorePlaystationGame(CancellationToken cancellationToken, PlaystationGameDto gamePlaystationDto, bool isIgnored);
        Task<ApiResult> ReloadPlaystationGame(CancellationToken cancellationToken, Guid gameDetailId);
        Task<ApiResult<List<PlaystationGameDto>>> GetMissingPlaystationGames(CancellationToken cancellationToken, bool forceReload = false);
        Task<ApiResult> RefreshToken(CancellationToken cancellationToken, string npsso);
    }
}
