using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPlaystationBL
    {
        Task<Guid> AddPlaystationGame(CreatePlaystationGameDto gamePlaystationDto);
        Task IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored);
        Task ReloadPlaystationGame(Guid gameDetailId);
        Task<List<PlaystationGameDto>> GetMissingPlaystationGames();
        Task<string> RefreshToken(string npsso);
    }
}
