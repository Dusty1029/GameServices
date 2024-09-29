using Game.Dto;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class CategoryExtensions
    {
        public static CategoryEntity ToEntity(this CategoryDto categoryDto) => new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };

        public static CategoryDto ToDto(this CategoryEntity categoryEntity) => new()
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name
        };
    }
}
