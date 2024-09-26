using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IPlaystationService
    {
        Task<ApiResult<Guid>> AddPlaystationGame(PlaystationGameDto gamePlaystationDto);
        Task<ApiResult> IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored);
        Task<ApiResult> ReloadPlaystationGame(Guid gameDetailId);
        Task<ApiResult<List<PlaystationGameDto>>> GetMissingPlaystationGames();
        Task<ApiResult> RefreshToken(string npsso);
    }
}
