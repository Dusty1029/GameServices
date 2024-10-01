using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResult<Guid>> CreateCategory(CancellationToken cancellationToken, string category);
        Task<ApiResult> DeleteCategory(CancellationToken cancellationToken, Guid categoryId);
        Task<ApiResult<List<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken);
        Task<ApiResult> UpdateCategory(CancellationToken cancellationToken, Guid categoryId, string category);
    }
}
