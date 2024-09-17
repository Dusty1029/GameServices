using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISteamBL
    {
        Task<Guid> AddSteamGame(SteamGameDto gameSteamDto);
        Task<List<SteamGameDto>> GetMissingSteamGames();
        Task<int> IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored);
        Task ReloadSteamGame(Guid gameId);
    }
}
