using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ICategoryBL
    {
        Task<Guid> CreateCategory(CategoryDto category);
        Task DeleteCategory(Guid categoryId);
        Task<List<CategoryDto>> GetAllCategories();
        Task UpdateCategory(Guid categoryId, CategoryDto category);
    }
}
