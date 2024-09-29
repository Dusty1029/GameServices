using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ICategoryBL
    {
        Task<Guid> CreateCategory(string category);
        Task DeleteCategory(Guid categoryId);
        Task<List<CategoryDto>> GetAllCategories();
        Task UpdateCategory(Guid categoryId, string category);
    }
}
