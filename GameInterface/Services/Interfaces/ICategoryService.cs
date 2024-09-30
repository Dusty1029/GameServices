using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResult<Guid>> CreateCategory(string category);
        Task<ApiResult> DeleteCategory(Guid categoryId);
        Task<ApiResult<List<CategoryDto>>> GetAllCategories();
        Task<ApiResult> UpdateCategory(Guid categoryId, string category);
    }
}
