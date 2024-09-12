using CommonV2.Models;
using Game.Dto;
using GameInterface.Extensions;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class GameService(HttpClient httpClient) : IGameService
    {
        public Task DeleteGameById(Guid gameId) => httpClient.Delete($"{gameId}");

        public Task<GameDto> GetGameById(Guid id) => httpClient.Get<GameDto>($"{id}");

        public Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto) => httpClient.Post<PaginationResult<GameDto>>(searchGameDto, "search");
    }
}
