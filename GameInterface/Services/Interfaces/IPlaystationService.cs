using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface IPlaystationService
    {
        Task<Guid> AddPlaystationGame(PlaystationGameDto gamePlaystationDto);
        Task IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored);
        Task ReloadPlaystationGame(Guid gameDetailId);
        Task<List<PlaystationGameDto>> GetMissingPlaystationGames();
        Task RefreshToken(string npsso);
    }
}
