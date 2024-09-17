using CommonV2.Models;
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface IGameBL
    {
        //Task<Guid> CreateGame(GameDto createGameDto);
        Task DeleteGameById(Guid gameDetailId);
        //Task<GameDto> GetGameById(Guid gameId);
        Task<PaginationResult<GameDto>> SearchGame(SearchGameDto searchGameDto);
        //Task UpdateGame(Guid gameId, GameDto gameDto);
    }
}
