using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class RoundExtensions
    {
        public static RoundDto ToDto(this RoundEntity entity) => new()
        {
            IsTeamRound = entity.IsTeamRound,
            GageName = entity.Gage!.Name,
            GameName = entity.Game!.Name,
            TeamOne = entity.TeamOne?.ToDto(),
            TeamTwo = entity.TeamTwo?.ToDto()
        };
    }
}
