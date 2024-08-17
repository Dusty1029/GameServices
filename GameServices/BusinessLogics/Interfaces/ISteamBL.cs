using GameServices.API.Dtos.SteamGateway;

namespace GameServices.API.BusinessLogics.Interfaces
{
    public interface ISteamBL
    {
        Task<Guid> AddSteamGame(GameSteamDto gameSteamDto);
        Task<List<GameSteamDto>?> GetMissingSteamGames();
        Task<Guid> IgnoreSteamGame(GameSteamDto gameSteamDto);
        Task ReloadSteamGame(Guid gameId);
    }
}
