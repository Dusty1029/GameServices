using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class GameExtensions
    {
        public static GameEntity ToEntity(this CreatePartyGameDto partyGame) => new()
        {
            Name = partyGame.Name,
            IsTeamGame = partyGame.IsTeamGame
        };

        public static PartyGameDto ToDto(this GameEntity game) => new()
        {
            Id = game.Id,
            Name = game.Name,
            IsTeamGame = game.IsTeamGame
        };
    }
}
