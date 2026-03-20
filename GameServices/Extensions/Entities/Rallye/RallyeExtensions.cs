using GameService.API.RallyeDtos;
using GameService.Infrastructure.Entities.Rallye;

namespace GameService.API.Extensions.Entities.Rallye
{
    public static class RallyeExtensions
    {
        public static RallyeDto ToSimpleDto(this RallyeEntity rallye) => new()
        {
            Id = rallye.Id,
            Name = rallye.Name
        };

        public static RallyeDto ToDto(this RallyeEntity rallye) => new()
        {
            Id = rallye.Id,
            Name = rallye.Name,
            Players = rallye.Players?.Select(p => p.ToDto()).ToList() ?? [],
            Specials = rallye.Specials?.Select(s => s.ToDto()).ToList() ?? []
        };

        public static RallyeEntity ToEntity(this CreateRallyeDto rallye) => new()
        {
            Name = rallye.Name
        };
    }
}
