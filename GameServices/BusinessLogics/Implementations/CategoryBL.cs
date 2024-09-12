using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;

namespace GameService.API.BusinessLogics.Implementations
{
    public class CategoryBL(ICategoryRepository categoryRepository) : ICategoryBL
    {
        public async Task<Guid> CreateCategory(CategoryDto category)
        {
            var categoryCreated = await categoryRepository.InsertAndSave(category.ToEntity());

            return categoryCreated.Id;
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await categoryRepository.Find(c => c.Id == categoryId);
            if (category is null)
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");
            if (category.IsSeed)
                throw new ValidationException($"The category with id [{categoryId}] is a seed and can't be deleted.");

            await categoryRepository.DeleteAndSave(category);
        }

        public Task<List<CategoryDto>> GetAllCategories()
            => categoryRepository.GetAllSelect(
                    f => f.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }),
                    orderBy: f => f.OrderBy(c => c.Name)
                );

        public async Task UpdateCategory(Guid categoryId, CategoryDto category)
        {
            var categoryEntity = await categoryRepository.Find(c => c.Id == categoryId, noTracking: false);
            if (categoryEntity is null)
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");

            category.ToEntity(categoryEntity);
            await categoryRepository.SaveChanges();
        }
    }
}
