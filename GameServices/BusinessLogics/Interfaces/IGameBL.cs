using CommonV2.Models;
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IGameBL
    {
        Task<Guid> AddPlatformToAGame(Guid gameId, Guid platformId);
        Task<Guid> CreateGame(CreateGameDto createGameDto);
        Task DeleteGameByGameDetailId(Guid gameDetailId);
        Task<GameDto> GetGameById(Guid gameId);
        Task<PaginationResult<SearchGameItemDto>> SearchGame(SearchGameDto searchGameDto);
        Task UpdateGame(Guid gameId, UpdateGameDto gameDto);
    }
}
