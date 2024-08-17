using CommonV2.Models;
using GameServices.API.Dtos;

namespace GameServices.API.BusinessLogics.Interfaces
{
    public interface IGameBL
    {
        Task<Guid> CreateGame(GameDto createGameDto);
        Task DeleteGameById(Guid gameId);
        Task<GameDto> GetGameById(Guid gameId);
        Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto);
        Task UpdateGame(Guid gameId, GameDto gameDto);
    }
}
