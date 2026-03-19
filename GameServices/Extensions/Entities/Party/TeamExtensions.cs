using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class TeamExtensions
    {
        public static TeamDto ToDto(this TeamEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Players = entity.Players?.Select(p => p.ToDto()).ToList()
        };
    }
}
