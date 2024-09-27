using CommonV2.Models;
using Game.Dto;
using Game.Dto.Enums;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class GameService(IGenericService genericService) : IGameService
    {
        private readonly string beginPath = "game";
        public Task<ApiResult<Guid>> AddPlatformToAGame(Guid gameId, Guid platformId) => genericService.PostResult<Guid>(path: $"{beginPath}/{gameId}/platform/{platformId}");
        public Task<ApiResult<Guid>> CreateGame(CreateGameDto createGameDto) => genericService.PostResult<Guid>(createGameDto, beginPath);
        public Task<ApiResult> DeleteGameById(Guid gameId) => genericService.DeleteResult($"{beginPath}/{gameId}");
        public Task<ApiResult<GameDto>> GetGameById(Guid id) => genericService.GetResult<GameDto>($"{beginPath}/{id}");
        public Task<ApiResult<PaginationResult<SearchGameItemDto>>> SearchGame(SearchGameDto searchGameDto) => genericService.PostResult<PaginationResult<SearchGameItemDto>>(searchGameDto, $"{beginPath}/search");
        public Task<ApiResult<List<SimpleGameDto>>> SearchSimpleGame(string gameSearched, PlatformEnumDto? platformEnum) => genericService.PostResult<List<SimpleGameDto>>(platformEnum, $"{beginPath}/search/{gameSearched}");
        public Task<ApiResult> UpdateGame(Guid gameId, UpdateGameDto gameDto) => genericService.PutResult(gameDto, $"{beginPath}/{gameId}");
    }
}
