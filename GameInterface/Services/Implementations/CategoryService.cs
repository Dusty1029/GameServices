using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class CategoryService(IGenericService genericService) : ICategoryService
    {
        private readonly string beginPath = "category";

        public Task<ApiResult<Guid>> CreateCategory(CancellationToken cancellationToken, string category) =>
            genericService.PostResult<Guid>(cancellationToken, category, beginPath);

        public Task<ApiResult> DeleteCategory(CancellationToken cancellationToken, Guid categoryId) =>
            genericService.DeleteResult(cancellationToken, $"{beginPath}/{categoryId}");

        public Task<ApiResult<List<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken) =>
            genericService.GetResult<List<CategoryDto>>(cancellationToken, beginPath);

        public Task<ApiResult> UpdateCategory(CancellationToken cancellationToken, Guid categoryId, string category) =>
            genericService.PutResult(cancellationToken, category, $"{beginPath}/{categoryId}");
    }
}
