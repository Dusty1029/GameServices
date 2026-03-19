using GameService.API.RallyeDtos;

namespace GameService.API.BusinessLogics.Interfaces.Rallye
{
    public interface IPlayerBL
    {
        Task<Guid> CreatePlayer(CreateRallyePlayerDto createPlayer);
        Task<List<RallyePlayerDto>> GetAllPlayers();
    }
}
