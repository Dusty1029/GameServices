using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class CategoryService(IGenericService genericService) : ICategoryService
    {
        private readonly string beginPath = "category";

        public Task<ApiResult<Guid>> CreateCategory(string category) => genericService.PostResult<Guid>(category, beginPath);

        public Task<ApiResult> DeleteCategory(Guid categoryId) => genericService.DeleteResult($"{beginPath}/{categoryId}");

        public Task<ApiResult<List<CategoryDto>>> GetAllCategories() => genericService.GetResult<List<CategoryDto>>(beginPath);

        public Task<ApiResult> UpdateCategory(Guid categoryId, string category) => genericService.PutResult(category, $"{beginPath}/{categoryId}");
    }
}
