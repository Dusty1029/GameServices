using CommonV2.Models;
using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface IGameService
    {
        Task<ApiResult<GameDto>> GetGameById(Guid id);
        Task<ApiResult<PaginationResult<SearchGameItemDto>>> SearchGame(SearchGameDto searchGameDto);
        Task<ApiResult> DeleteGameById(Guid gameId);
        Task<ApiResult<Guid>> CreateGame(CreateGameDto createGameDto);
        Task<ApiResult> UpdateGame(Guid gameId, UpdateGameDto gameDto);
        Task<ApiResult<Guid>> AddPlatformToAGame(Guid gameId, Guid platformId);
    }
}
