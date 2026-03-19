using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class PlayerExtensions
    {
        public static PlayerEntity ToEntity(this CreatePlayerDto createPlayer) => new()
        {
            Name = createPlayer.Name
        };

        public static PlayerDto ToDto(this PlayerEntity playerEntity) => new()
        {
            Id = playerEntity.Id,
            Name = playerEntity.Name,
        };
    }
}
