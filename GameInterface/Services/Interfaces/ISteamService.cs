using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface ISteamService
    {
        Task<Guid> AddSteamGame(SteamGameDto gameSteamDto);
        Task<List<SteamGameDto>> GetMissingSteamGames();
        Task IgnoreSteamGame(SteamGameDto gameSteamDto, bool isIgnored);
        Task ReloadSteamGame(Guid gameDetailId);
    }
}
