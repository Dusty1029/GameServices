using CommonV2.Models;
using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class GameService(HttpClient httpClient) : IGameService
    {
        public Task<Guid> AddPlatformToAGame(Guid gameId, Guid platformId) => httpClient.Post<Guid>(path: $"{gameId}/platform/{platformId}");
        public Task<Guid> CreateGame(CreateGameDto createGameDto) => httpClient.Post<Guid>(createGameDto);
        public Task DeleteGameById(Guid gameId) => httpClient.Delete($"{gameId}");
        public Task<GameDto> GetGameById(Guid id) => httpClient.Get<GameDto>($"{id}");
        public Task<PaginationResult<SearchGameItemDto>> SearchGame(SearchGameDto searchGameDto) => httpClient.Post<PaginationResult<SearchGameItemDto>>(searchGameDto, "search");
        public Task UpdateGame(Guid gameId, UpdateGameDto gameDto) => httpClient.Put(gameDto, $"{gameId}");
    }
}
