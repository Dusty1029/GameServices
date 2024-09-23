using CommonV2.Models;
using Game.Dto;

namespace GameInterface.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetGameById(Guid id);
        Task<PaginationResult<SearchGameItemDto>> SearchGame(SearchGameDto searchGameDto);
        Task DeleteGameById(Guid gameId);
        Task<Guid> CreateGame(CreateGameDto createGameDto);
        Task UpdateGame(Guid gameId, UpdateGameDto gameDto);
        Task<Guid> AddPlatformToAGame(Guid gameId, Guid platformId);
    }
}
