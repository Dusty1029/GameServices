using CommonV2.Models;
using Game.Dto.Enums;
using Game.Dto.Games;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IGameService
    {
        Task<ApiResult<GameDto>> GetGameById(CancellationToken cancellationToken, Guid id);
        Task<ApiResult<PaginationResult<SearchGameItemDto>>> SearchGame(CancellationToken cancellationToken, SearchGameDto searchGameDto);
        Task<ApiResult> DeleteGameById(CancellationToken cancellationToken, Guid gameId);
        Task<ApiResult<Guid>> CreateGame(CancellationToken cancellationToken, CreateGameDto createGameDto);
        Task<ApiResult> UpdateGame(CancellationToken cancellationToken, Guid gameId, UpdateGameDto gameDto);
        Task<ApiResult<Guid>> AddPlatformToAGame(CancellationToken cancellationToken, Guid gameId, Guid platformId);
        Task<ApiResult<List<SimpleGameDto>>> SearchSimpleGame(CancellationToken cancellationToken, string gameSearched, PlatformEnumDto? platformEnum);
        Task<ApiResult> UpdateGameTime(CancellationToken cancellationToken, Guid gameId);
    }
}
