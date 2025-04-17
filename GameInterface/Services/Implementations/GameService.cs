using CommonV2.Models;
using Game.Dto.Enums;
using Game.Dto.Games;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class GameService(IGenericService genericService) : IGameService
    {
        private readonly string beginPath = "game";
        public Task<ApiResult<Guid>> AddPlatformToAGame(CancellationToken cancellationToken, Guid gameId, Guid platformId) =>
            genericService.PostResult<Guid>(cancellationToken, path: $"{beginPath}/{gameId}/platform/{platformId}");
        public Task<ApiResult<Guid>> CreateGame(CancellationToken cancellationToken, CreateGameDto createGameDto) =>
            genericService.PostResult<Guid>(cancellationToken, createGameDto, beginPath);
        public Task<ApiResult> DeleteGameById(CancellationToken cancellationToken, Guid gameId) =>
            genericService.DeleteResult(cancellationToken, $"{beginPath}/{gameId}");
        public Task<ApiResult<GameDto>> GetGameById(CancellationToken cancellationToken, Guid id) =>
            genericService.GetResult<GameDto>(cancellationToken, $"{beginPath}/{id}");
        public Task<ApiResult<PaginationResult<SearchGameItemDto>>> SearchGame(CancellationToken cancellationToken, SearchGameDto searchGameDto) =>
            genericService.PostResult<PaginationResult<SearchGameItemDto>>(cancellationToken, searchGameDto, $"{beginPath}/search");
        public Task<ApiResult<List<SimpleGameDto>>> SearchSimpleGame(CancellationToken cancellationToken, string gameSearched, PlatformEnumDto? platformEnum) =>
            genericService.PostResult<List<SimpleGameDto>>(cancellationToken, platformEnum, $"{beginPath}/search/{gameSearched}");
        public Task<ApiResult> UpdateGame(CancellationToken cancellationToken, Guid gameId, UpdateGameDto gameDto) =>
            genericService.PutResult(cancellationToken, gameDto, $"{beginPath}/{gameId}");
        public Task<ApiResult> UpdateGameTime(CancellationToken cancellationToken, Guid gameId) =>
            genericService.PutResult(cancellationToken, path: $"{beginPath}/{gameId}/time");
    }
}
