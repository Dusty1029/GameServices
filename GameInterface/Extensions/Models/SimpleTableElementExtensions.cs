using Game.Dto;
using Game.Dto.Series;
using GameInterface.Models;

namespace GameInterface.Extensions.Models
{
    public static class SimpleTableElementExtensions
    {
        public static SimpleTableElement ToSimpleTableElement(this CategoryDto categoryDto) => new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name,
            CanBeDeleted = categoryDto.CanBeDeleted
        };

        public static SimpleTableElement ToSimpleTableElement(this PlatformDto platformDto) => new()
        {
            Id = platformDto.Id,
            Name = platformDto.Name,
            CanBeDeleted = platformDto.CanBeDeleted
        };

        public static SimpleTableElement ToSimpleTableElement(this SimpleSerieDto serieDto) => new()
        {
            Id = serieDto.Id,
            Name = serieDto.Serie,
            CanBeDeleted = serieDto.CanBeDeleted
        };
    }
}
