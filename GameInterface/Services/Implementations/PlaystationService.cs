using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class PlaystationService(HttpClient httpClient) : IPlaystationService
    {
        public Task<Guid> AddPlaystationGame(PlaystationGameDto gamePlaystationDto) => httpClient.Post<Guid>(gamePlaystationDto);

        public Task<List<PlaystationGameDto>> GetMissingPlaystationGames() => httpClient.Get<List<PlaystationGameDto>>();

        public Task IgnorePlaystationGame(PlaystationGameDto gamePlaystationDto, bool isIgnored) => httpClient.Post(gamePlaystationDto, $"ignore/{isIgnored}");

        public Task RefreshToken(string npsso)
        {
            throw new NotImplementedException();
        }

        public Task ReloadPlaystationGame(Guid gameDetailId) => httpClient.Put(path: $"game/{gameDetailId}/reload");
    }
}
