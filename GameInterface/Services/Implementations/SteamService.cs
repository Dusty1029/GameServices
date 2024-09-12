using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class SteamService(HttpClient httpClient) : ISteamService
    {
        public Task<Guid> AddSteamGame(SteamGameDto gameSteamDto) => httpClient.Post<Guid>(gameSteamDto);

        public Task<List<SteamGameDto>> GetMissingSteamGames() => httpClient.Get<List<SteamGameDto>>();
    }
}
