using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class PartyExtensions
    {
        public static SimplePartyDto ToSimpleDto(this PartyEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsFinish = entity.IsFinish
        };

        public static PartyDto ToDto(this PartyEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            ActualRound = entity.ActualRound,
            IsFinish = entity.IsFinish,
            Round = entity.Rounds!.First(r => r.Order == entity.ActualRound).ToDto(),
            Teams = entity.Teams!.Select(t => t.ToDto()).ToList()
        };
    }
}
