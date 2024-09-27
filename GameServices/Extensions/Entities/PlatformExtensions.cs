using Game.Dto;
using GameService.API.Extensions.Entities.Enums;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class PlatformExtensions
    {
        public static PlatformDto ToDto(this PlatformEntity platformEntity) => new()
        {
            Id = platformEntity.Id,
            Name = platformEntity.Name,
            PlatformEnum = platformEntity.PlatformEnum?.ToDto()
        };
    }
}
