using CommonV2.Models;
using Game.Dto.Enums;
using Game.Dto.Games;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IGameBL
    {
        Task<Guid> AddPlatformToAGame(Guid gameId, Guid platformId);
        Task<Guid> CreateGame(CreateGameDto createGameDto);
        Task DeleteGameByGameDetailId(Guid gameDetailId);
        Task<GameDto> GetGameById(Guid gameId);
        Task<PaginationResult<SearchGameItemDto>> SearchGame(SearchGameDto searchGameDto);
        Task<List<SimpleGameDto>> SearchSimpleGame(string gameSearched, PlatformEnumDto? ignoredPlatform);
        Task UpdateGame(Guid gameId, UpdateGameDto gameDto);
        Task UpdateGameTime(Guid gameId);
    }
}
