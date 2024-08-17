﻿using GameService.Infrastructure.Entities;
using GameServices.API.Dtos;

namespace GameServices.API.Extensions.Entities
{
    public static class CategoryExtensions
    {
        public static CategoryEntity ToEntity(this CategoryDto categoryDto, CategoryEntity? categoryEntity = null) 
        {
            categoryEntity ??= new();

            categoryEntity.Id = categoryDto.Id;
            categoryEntity.Name = categoryDto.Name;

            return categoryEntity;
        }

        public static CategoryDto ToDto(this CategoryEntity categoryEntity) => new()
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name
        };
    }
}
