using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISteamBL
    {
        Task<Guid> AddSteamGame(SteamGameDto gameSteamDto);
        Task<List<SteamGameDto>> GetMissingSteamGames();
        Task<Guid> IgnoreSteamGame(SteamGameDto gameSteamDto);
        Task ReloadSteamGame(Guid gameId);
    }
}
