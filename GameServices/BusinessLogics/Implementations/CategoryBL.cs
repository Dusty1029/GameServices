using CommonV2.Models.Exceptions;
using GameService.Infrastructure.Repositories.Interfaces;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Dtos;
using GameServices.API.Extensions.Entities;

namespace GameServices.API.BusinessLogics.Implementations
{
    public class CategoryBL : ICategoryBL
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBL(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> CreateCategory(CategoryDto category)
        {
            var categoryCreated = await _categoryRepository.InsertAndSave(category.ToEntity());

            return categoryCreated.Id;
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _categoryRepository.Find(c => c.Id == categoryId);
            if (category is null)
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");
            if (category.IsSeed)
                throw new ValidationException($"The category with id [{categoryId}] is a seed and can't be deleted.");

            await _categoryRepository.DeleteAndSave(category);
        }

        public Task<List<CategoryDto>> GetAllCategories()
            => _categoryRepository.GetAllSelect(
                    f => f.Select(c => new CategoryDto { Id = c.Id, Name = c.Name}),
                    orderBy: f => f.OrderBy(c => c.Name)
                );

        public async Task UpdateCategory(Guid categoryId, CategoryDto category)
        {
            var categoryEntity = await _categoryRepository.Find(c => c.Id == categoryId, noTracking: false);
            if (categoryEntity is null)
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");

            category.ToEntity(categoryEntity);
            await _categoryRepository.SaveChanges();
        }
    }
}
