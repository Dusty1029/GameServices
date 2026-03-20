using GameService.API.RallyeDtos;
using GameService.Infrastructure.Entities.Rallye;

namespace GameService.API.Extensions.Entities.Rallye
{
    public static class SpecialExtensions
    {
        public static SpecialDto ToDto(this SpecialEntity special) => new()
        {
            Id = special.Id,
            Name = special.Name,
            SpecialTimes = special.SpecialTimes?.Select(st => st.ToDto()).ToList() ?? []
        };

        public static SpecialEntity ToEntity(this CreateSpecialDto special) => new()
        {
            Name = special.Name,
            RallyeId = special.RallyeId
        };
    }
}
