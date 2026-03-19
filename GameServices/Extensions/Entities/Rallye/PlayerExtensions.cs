using GameService.API.RallyeDtos;
using GameService.Infrastructure.Entities.Rallye;

namespace GameService.API.Extensions.Entities.Rallye
{
    public static class PlayerExtensions
    {
        public static PlayerEntity ToEntity(this CreateRallyePlayerDto createPlayer) => new()
        {
            Name = createPlayer.Name
        };

        public static RallyePlayerDto ToDto(this PlayerEntity playerEntity) => new()
        {
            Id = playerEntity.Id,
            Name = playerEntity.Name,
        };
    }
}
