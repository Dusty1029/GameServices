using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.Infrastructure.Repositories.Interfaces;
using GameService.API.BusinessLogics.Interfaces;

namespace GameService.API.BusinessLogics.Implementations
{
    public class CategoryBL(ICategoryRepository categoryRepository) : ICategoryBL
    {
        public async Task<Guid> CreateCategory(string category) =>
           (await categoryRepository.InsertAndSave(new() { Name = category })).Id;
        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await categoryRepository.Find(c => c.Id == categoryId) ??
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");
            if (category.IsSeed)
                throw new ValidationException($"The category with id [{categoryId}] is a seed and can't be deleted.");

            await categoryRepository.DeleteAndSave(category);
        }

        public Task<List<CategoryDto>> GetAllCategories()
            => categoryRepository.GetAllSelect(
                    f => f.Select(c => new CategoryDto { Id = c.Id, Name = c.Name, CanBeDeleted = !c.IsSeed }),
                    orderBy: f => f.OrderBy(c => c.Name)
                );

        public async Task UpdateCategory(Guid categoryId, string category)
        {
            var categoryEntity = await categoryRepository.Find(c => c.Id == categoryId && !c.IsSeed, noTracking: false) ??
                throw new NotFoundException($"The category with id [{categoryId}] was not found.");

            categoryEntity.Name = category;
            await categoryRepository.SaveChanges();
        }
    }
}
