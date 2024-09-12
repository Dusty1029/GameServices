using GameService.API.Models.PlaystationGateway;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IPlaystationBL
    {
        Task<Guid> AddPlaystationGame(GamePlaystation gamePlaystationDto);
        Task<List<GamePlaystation>?> GetMissingPlaystationGames();
        Task<Guid> IgnorePlaystationGame(GamePlaystation gameSteamDto);
        Task RefreshToken(string npsso);
        Task ReloadPlaystationGame(Guid gameId);
    }
}
