using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISteamBL
    {
        Task<Guid> AddSteamGame(CreateSteamGameDto gameSteamDto);
        Task<List<SteamGameDto>> GetMissingSteamGames();
        Task IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored);
        Task ReloadSteamGame(Guid gameId);
    }
}
