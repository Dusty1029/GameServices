using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface ISteamService
    {
        Task<List<SteamGameDto>> GetMissingSteamGames();
        Task<Guid> AddSteamGame(SteamGameDto gameSteamDto);
    }
}
