using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;

namespace GameService.API.Extensions.Entities.Party
{
    public static class GageExtensions
    {
        public static GageEntity ToEntity(this CreateGageDto gageDto) => new()
        {
            Name = gageDto.Name
        };

        public static GageDto ToDto(this GageEntity gage) => new()
        {
            Id = gage.Id,
            Name = gage.Name
        };
    }
}
