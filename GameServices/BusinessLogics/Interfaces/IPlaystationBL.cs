using GameServices.API.Dtos.PlaystationGateway;

namespace GameServices.API.BusinessLogics.Interfaces
{
    public interface IPlaystationBL
    {
        Task<Guid> AddPlaystationGame(GamePlaystationDto gamePlaystationDto);
        Task<List<GamePlaystationDto>?> GetMissingPlaystationGames();
        Task<Guid> IgnorePlaystationGame(GamePlaystationDto gameSteamDto);
        Task RefreshToken(string npsso);
        Task ReloadPlaystationGame(Guid gameId);
    }
}
