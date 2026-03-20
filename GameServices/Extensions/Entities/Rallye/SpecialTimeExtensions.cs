using GameService.API.RallyeDtos;
using GameService.Infrastructure.Entities.Rallye;

namespace GameService.API.Extensions.Entities.Rallye
{
    public static class SpecialTimeExtensions
    {
        public static SpecialTimeDto ToDto(this SpecialTimeEntity specialTime) => new()
        {
            Id = specialTime.Id,
            Player = specialTime.Player!.ToDto(),
            Time = specialTime.Time
        };

        public static SpecialTimeEntity ToEntity(this CreateSpecialTimeDto specialTime) => new()
        {
            Time = specialTime.Time,
            PlayerId = specialTime.PlayerId,
            SpecialId = specialTime.SpecialId
        };
    }
}
