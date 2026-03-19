using GameService.API.BusinessLogics.Interfaces.Rallye;
using GameService.API.Extensions.Entities.Rallye;
using GameService.API.RallyeDtos;
using GameService.Infrastructure.Repositories.Interfaces.Rallye;

namespace GameService.API.BusinessLogics.Implementations.Rallye
{
    public class PlayerBL(IPlayerRepository playerRepository) : IPlayerBL
    {
        public async Task<Guid> CreatePlayer(CreateRallyePlayerDto createPlayer) =>
            (await playerRepository.InsertAndSave(createPlayer.ToEntity())).Id;

        public async Task<List<RallyePlayerDto>> GetAllPlayers() =>
            (await playerRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToDto()).ToList();
    }
}
